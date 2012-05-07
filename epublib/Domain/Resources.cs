///////////////////////////////////////////////////////////
//  Resources.cs
//  Implementation of the Class Resources
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:05
//  Original author: lanfengqi
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using epublib;
using nl.siegmann.epublib.domain;
using nl.siegmann.epublib.service;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.domain
{
    /// <summary>
    /// All the resources that make up the book. XHTML files, images and epub xml
    /// documents must be here.
    /// </summary>
    [Serializable]
    public class Resources
    {

        private static readonly string IMAGE_PREFIX = "image_";
        private static readonly string ITEM_PREFIX = "item_";
        private int lastId = 1;
        private Dictionary<String, Resource> resources = new Dictionary<String, Resource>();
        private static readonly long serialVersionUID = 2450876953383871451L;


        /// <summary>
        /// Adds a resource to the resources.  Fixes the resources id and href if necessary.
        /// 
        /// </summary>
        /// <param name="resource"></param>
        public Resource add(Resource resource)
        {
            fixResourceHref(resource);
            fixResourceId(resource);
            resources.Add(resource.getHref(), resource);
            return resource;
        }

        /// 
        /// <param name="resource"></param>
        private void fixResourceHref(Resource resource)
        {
            if (StringUtil.isNotBlank(resource.getHref())
                   && !resources.ContainsKey(resource.getHref()))
            {
                return;
            }
            if (StringUtil.isBlank(resource.getHref()))
            {
                if (resource.getMediaType() == null)
                {
                    throw new ArgumentException("Resource must have either a MediaType or a href");
                }
                int i = 1;
                String href = createHref(resource.getMediaType(), i);
                while (resources.ContainsKey(href))
                {
                    href = createHref(resource.getMediaType(), (++i));
                }
                resource.setHref(href);
            }
        }

        /// <summary>
        /// Checks the id of the given resource and changes to a unique identifier if it
        /// isn't one already.
        /// </summary>
        /// <param name="resource">resource</param>
        public void fixResourceId(Resource resource)
        {

            string resourceId = resource.getId();
            if (StringUtil.isBlank(resource.getId()))
            {
                resourceId = StringUtil.substringBeforeLast(resource.getHref(), '.');
                resourceId = StringUtil.substringAfterLast(resourceId, '/');
            }

            resourceId = makeValidId(resourceId, resource);

            // check if the id is unique. if not: create one from scratch
            if (StringUtil.isBlank(resourceId) || containsId(resourceId))
            {
                resourceId = createUniqueResourceId(resource);
            }
            resource.setId(resourceId);
        }


        /// <summary>
        /// Whether there exists a resource with the given href
        /// </summary>
        /// <param name="href"></param>
        public bool containsByHref(string href)
        {
            if (StringUtil.isBlank(href))
            {
                return false;
            }
            return resources.ContainsKey(StringUtil.substringBefore(href, Constants.FRAGMENT_SEPARATOR_CHAR));
        }

        /// <summary>
        /// Whether the map of resources already contains a resource with the given id.
        /// </summary>
        /// <param name="id"></param>
        public bool containsId(string id)
        {
            if (StringUtil.isBlank(id))
            {
                return false;
            }
            foreach (Resource resource in resources.Values)
            {
                if (id.Equals(resource.getId()))
                {
                    return true;
                }
            }
            return false;
        }

        /// 
        /// <param name="mediaType"></param>
        /// <param name="counter"></param>
        private string createHref(MediaType mediaType, int counter)
        {
            if (MediatypeService.isBitmapImage(mediaType))
            {
                return "image_" + counter + mediaType.getDefaultExtension();
            }
            else
            {
                return "item_" + counter + mediaType.getDefaultExtension();
            }
        }

        /// <summary>
        /// Creates a new resource id that is guarenteed to be unique for this set of
        /// Resources
        /// </summary>
        /// <param name="resource"></param>
        private string createUniqueResourceId(Resource resource)
        {
            int counter = lastId;
            if (counter == Int32.MaxValue)
            {
                if (resources.Keys.Count == Int32.MaxValue)
                {
                    throw new ArgumentException("Resources contains " + Int32.MaxValue + " elements: no new elements can be added");
                }
                else
                {
                    counter = 1;
                }
            }
            String prefix = getResourceItemPrefix(resource);
            String result = prefix + counter;
            while (containsId(result))
            {
                result = prefix + (++counter);
            }
            lastId = counter;
            return result;
        }

        /// <summary>
        /// Gets the first resource (random order) with the give mediatype.  Useful for
        /// looking up the table of contents as it's supposed to be the only resource with
        /// NCX mediatype.
        /// </summary>
        /// <param name="mediaType"></param>
        public Resource findFirstResourceByMediaType(MediaType mediaType)
        {
            return findFirstResourceByMediaType(resources, mediaType);
        }

        /// <summary>
        /// Gets the first resource (random order) with the give mediatype.  Useful for
        /// looking up the table of contents as it's supposed to be the only resource with
        /// NCX mediatype.
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="mediaType"></param>
        public static Resource findFirstResourceByMediaType(Dictionary<string, Resource> resources, MediaType mediaType)
        {
            foreach (Resource resource in resources.Values)
            {
                if (resource.getMediaType() == mediaType)
                {
                    return resource;
                }
            }
            return null;
        }



        public IEnumerable<Resource> getAll()
        {
            return resources.Values;
        }

        public IEnumerable<String> getAllHrefs()
        {
            return resources.Keys;
        }

        /// <summary>
        /// Gets the resource with the given href. If the given href contains a fragmentId
        /// then that fragment id will be ignored.
        /// </summary>
        /// null if not found.
        /// <param name="href"></param>
        public Resource getByHref(string href)
        {
            if (StringUtil.isBlank(href))
            {
                return null;
            }
            href = StringUtil.substringBefore(href, Constants.FRAGMENT_SEPARATOR_CHAR);
            Resource result = resources[href];
            return result;
        }

        /// <summary>
        /// Gets the resource with the given id.
        /// </summary>
        /// null if not found
        /// <param name="id"></param>
        public Resource getById(string id)
        {
            if (StringUtil.isBlank(id))
            {
                return null;
            }
            foreach (Resource resource in resources.Values)
            {
                if (id.Equals(resource.getId()))
                {
                    return resource;
                }
            }
            return null;
        }

        /// <summary>
        /// First tries to find a resource with as id the given idOrHref, if that fails it
        /// tries to find one with the idOrHref as href.
        /// </summary>
        /// <param name="idOrHref"></param>
        public Resource getByIdOrHref(string idOrHref)
        {
            Resource resource = getById(idOrHref);
            if (resource == null)
            {
                resource = getByHref(idOrHref);
            }
            return resource;
        }

        /// 
        /// <param name="resource"></param>
        private string getResourceItemPrefix(Resource resource)
        {
            String result;
            if (MediatypeService.isBitmapImage(resource.getMediaType()))
            {
                result = IMAGE_PREFIX;
            }
            else
            {
                result = ITEM_PREFIX;
            }
            return result;
        }

        /// <summary>
        /// The resources that make up this book. Resources can be xhtml pages, images, xml
        /// documents, etc.
        /// </summary>
        public Dictionary<String, Resource> getResourceMap()
        {
            return resources;
        }

        /// <summary>
        /// All resources that have the given MediaType.
        /// </summary>
        /// <param name="mediaType"></param>
        public List<Resource> getResourcesByMediaType(MediaType mediaType)
        {
            List<Resource> result = new List<Resource>();
            if (mediaType == null)
            {
                return result;
            }
            foreach (Resource resource in getAll())
            {
                if (resource.getMediaType() == mediaType)
                {
                    result.Add(resource);
                }
            }
            return result;
        }

        /// <summary>
        /// All Resources that match any of the given list of MediaTypes
        /// </summary>
        /// <param name="mediaTypes"></param>
        public List<Resource> getResourcesByMediaTypes(MediaType[] mediaTypes)
        {
            List<Resource> result = new List<Resource>();
            if (mediaTypes == null)
            {
                return result;
            }
            List<MediaType> lmediaType = new List<MediaType>(mediaTypes);
     
            foreach (Resource resource in getAll())
            {
                if (lmediaType.Contains(resource.getMediaType()))
                {
                    result.Add(resource);
                }
            }
            return result;
        }

        public bool isEmpty()
        {
            return resources.Count == 0 ? true : false;
        }

        /// <summary>
        /// Check if the id is a valid identifier. if not: prepend with valid identifier
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="resource"></param>
        private string makeValidId(string resourceId, Resource resource)
        {
            if (StringUtil.isNotBlank(resourceId))
            {
                resourceId = getResourceItemPrefix(resource) + resourceId;
            }
            return resourceId;
        }

        /// <summary>
        /// Remove the resource with the given href.
        /// </summary>
        /// the removed resource, null if not found
        /// <param name="href"></param>
        public Resource remove(string href)
        {
            Resource resource = resources[href];
            if (resource != null)
            {
                resources.Remove(href);
                return resource;
            }
            return null;
        }

        /// <summary>
        /// Sets the collection of Resources to the given collection of resources
        /// </summary>
        /// <param name="resources">resources</param>
        public void set(List<Resource> resources)
        {
            this.resources.Clear();
            AddAll(resources);
        }

        /// <summary>
        /// Sets the collection of Resources to the given collection of resources
        /// </summary>
        /// <param name="resources">A map with as keys the resources href and as values the
        /// Resources</param>
        public void AddAll(List<Resource> resources)
        {
            foreach (Resource resource in resources)
            {
                fixResourceHref(resource);
                this.resources.Add(resource.getHref(), resource);
            }

        }

        /// <summary>
        /// The number of resources
        /// </summary>
        public int size()
        {
            return resources.Count;
        }

    }//end Resources

}//end namespace domain