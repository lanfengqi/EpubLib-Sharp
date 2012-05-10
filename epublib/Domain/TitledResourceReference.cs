///////////////////////////////////////////////////////////
//  TitledResourceReference.cs
//  Implementation of the Class TitledResourceReference
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:08
//  Original author: lanfengqi
///////////////////////////////////////////////////////////


using System;
using nl.siegmann.epublib.domain;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.domain
{
    [Serializable]
    public class TitledResourceReference : ResourceReference
    {

        private string fragmentId;
        private static readonly long serialVersionUID = 3918155020095190080L;
        private string title;

        public TitledResourceReference(Resource resource)
            : this(resource, null)
        {

        }

        public TitledResourceReference(Resource resource, string title)
            : this(resource, title, null)
        {

        }

        public TitledResourceReference(Resource resource, string title, string fragmentId)
            : base(resource)
        {
            this.title = title;
            this.fragmentId = fragmentId;
        }

        public string getFragmentId()
        {
            return this.fragmentId;
        }

        public void setFragmentId(string fragmentId)
        {
            this.fragmentId = fragmentId;
        }

        public string getTitle()
        {
            return this.title;
        }

        public void setTitle(string title)
        {
            this.setTitle(title);
        }

        /// <summary>
        /// If the fragmentId is blank it returns the resource href, otherwise it returns
        /// the resource href + '#' + the fragmentId.
        /// </summary>
        public string getCompleteHref()
        {
            if (StringUtil.isBlank(fragmentId))
            {
                return resource.getHref();
            }
            else
            {
                return resource.getHref() + Constants.FRAGMENT_SEPARATOR_CHAR + fragmentId;
            }
        }

        /// <summary>
        /// Sets the resource to the given resource and sets the fragmentId to null.
        /// </summary>
        /// <param name="resource"></param>
        public void setResource(Resource resource, string fragmentId)
        {
            base.setResource(resource);
            this.fragmentId = fragmentId;
        }

        /// <summary>
        /// Sets the resource to the given resource and sets the fragmentId to null.
        /// </summary>
        /// <param name="resource"></param>
        public void setResource(Resource resource)
        {
            setResource(resource, null);
        }
    }
}