///////////////////////////////////////////////////////////
//  Resource.cs
//  Implementation of the Class Resource
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:04
//  Original author: lanfengqi
///////////////////////////////////////////////////////////


using System;
using System.IO;
using Ionic.Zip;
using epublib;
using nl.siegmann.epublib.domain;
using nl.siegmann.epublib.service;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.domain
{
    /// <summary>
    /// Represents a resource that is part of the epub. A resource can be a html file,
    /// image, xml, etc.
    /// </summary>
    [Serializable]
    public class Resource
    {

        private long cachedSize;
        private byte[] data;
        private string fileName;
        private string href;
        private string id;
        private string inputEncoding = Constants.ENCODING;
        //private static readonly Logger LOG = LoggerFactory.getLogger(Resource.class);
        private MediaType mediaType;
        private static readonly long serialVersionUID = 1043946707835004037L;
        private string title;

        /// <summary>
        /// Creates an empty Resource with the given href.  Assumes that if the data is of
        /// a text type (html/css/etc) then the encoding will be UTF-8
        /// </summary>
        /// <param name="href">The location of the resource within the epub. Example:
        /// "chapter1.html".</param>
        public Resource(string href)
            : this(null, new byte[0], href, MediatypeService.determineMediaType(href))
        {

        }

        /// <summary>
        /// Creates a Resource with the given data and MediaType. The href will be
        /// automatically generated.  Assumes that if the data is of a text type
        /// (html/css/etc) then the encoding will be UTF-8
        /// </summary>
        /// <param name="data">The Resource's contents</param>
        /// <param name="mediaType">The MediaType of the Resource</param>
        public Resource(byte[] data, MediaType mediaType)
            : this(null, data, null, mediaType)
        {

        }

        /// <summary>
        /// Creates a resource with the given data at the specified href. The MediaType
        /// will be determined based on the href extension.  Assumes that if the data is of
        /// a text type (html/css/etc) then the encoding will be UTF-8
        /// </summary>
        /// <see>nl.siegmann.epublib.service.MediatypeService.
        /// determineMediaType(String)</see>
        /// <param name="data">The Resource's contents</param>
        /// <param name="href">The location of the resource within the epub. Example:
        /// "chapter1.html".</param>
        public Resource(byte[] data, string href)
            : this(null, data, href, MediatypeService.determineMediaType(href), Constants.ENCODING)
        {

        }


        /// <summary>
        /// Creates a resource with the data from the given InputStream at the specified
        /// href. The MediaType will be determined based on the href extension.
        /// </summary>
        /// <see>nl.siegmann.epublib.service.MediatypeService.determineMediaType(String)
        /// Assumes that if the data is of a text type (html/css/etc) then the encoding
        /// will be UTF-8  It is recommended to us the</see>
        /// <see>nl.siegmann.epublib.domain.Resource.Resource(Reader, String) method for
        /// creating textual (html/css/etc) resources to prevent encoding problems. Use
        /// this method only for binary Resources like images, fonts, etc.</see>
        /// <param name="in">The Resource's contents</param>
        /// <param name="href">The location of the resource within the epub. Example:
        /// "cover.jpg".</param>
        public Resource(System.IO.Stream stream, string href)
            : this(null, IOUtil.toByteArray(stream), href, MediatypeService.determineMediaType(href))
        {

        }

        /// <summary>
        /// Creates a Lazy resource, by not actually loading the data for this entry.  The
        /// data will be loaded on the first call to getData()
        /// </summary>
        /// <param name="fileName">the fileName for the epub we're created from.</param>
        /// <param name="size">the size of this resource.</param>
        /// <param name="href">The resource's href within the epub.</param>
        public Resource(string fileName, long size, string href)
            : this(null, null, href, MediatypeService.determineMediaType(href))
        {
            this.fileName = fileName;
            this.cachedSize = size;
        }

        /// <summary>
        /// Creates a resource with the given id, data, mediatype at the specified href.
        /// Assumes that if the data is of a text type (html/css/etc) then the encoding
        /// will be UTF-8
        /// </summary>
        /// <param name="id">The id of the Resource. Internal use only. Will be auto-
        /// generated if it has a null-value.</param>
        /// <param name="data">The Resource's contents</param>
        /// <param name="href">The location of the resource within the epub. Example:
        /// "chapter1.html".</param>
        /// <param name="mediaType">The resources MediaType</param>
        public Resource(string id, byte[] data, string href, MediaType mediaType)
            : this(id, data, href, mediaType, Constants.ENCODING)
        {

        }

        /// <summary>
        /// Creates a resource with the given id, data, mediatype at the specified href. If
        /// the data is of a text type (html/css/etc) then it will use the given
        /// inputEncoding.
        /// </summary>
        /// <param name="id">The id of the Resource. Internal use only. Will be auto-
        /// generated if it has a null-value.</param>
        /// <param name="data">The Resource's contents</param>
        /// <param name="href">The location of the resource within the epub. Example:
        /// "chapter1.html".</param>
        /// <param name="mediaType">The resources MediaType</param>
        /// <param name="inputEncoding">If the data is of a text type (html/css/etc) then
        /// it will use the given inputEncoding.</param>
        public Resource(string id, byte[] data, string href, MediaType mediaType, string inputEncoding)
        {
            this.id = id;
            this.href = href;
            this.mediaType = mediaType;
            this.inputEncoding = inputEncoding;
            this.data = data;
        }

        /// <summary>
        /// Tells this resource to release its cached data.  If this resource was not lazy-
        /// loaded, this is a no-op.
        /// </summary>
        public void close()
        {
            if (this.fileName != null)
            {
                this.data = null;
            }
        }

        /// <summary>
        /// Checks to see of the given resourceObject is a resource and whether its href is
        /// equal to this one.
        /// </summary>
        /// <param name="resourceObject"></param>
        public bool equals(Object resourceObject)
        {
            if (!(resourceObject.GetType() == typeof(Resource)))
            {
                return false;
            }
            return href.Equals(((Resource)resourceObject).getHref());
        }

        /// <summary>
        /// The contents of the resource as a byte[]  If this resource was lazy-loaded and
        /// the data was not yet loaded, it will be loaded into memory at this point. This
        /// included opening the zip file, so expect a first load to be slow.
        /// </summary>
        /// The contents of the resource
        public byte[] getData()
        {
            if (data == null)
            {
                ZipFile _zipFile = ZipFile.Read(this.fileName);
                foreach (ZipEntry zipEntry in _zipFile)
                {
                    if (!zipEntry.IsDirectory)
                    {
                        if (zipEntry.FileName.Equals(this.href))
                        {
                            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                            {
                                zipEntry.Extract(memoryStream);
                                memoryStream.Position = 0L;
                                this.data = IOUtil.toByteArray(memoryStream);
                            }
                        }
                    }
                }
            }

            return data;

        }

        /// <summary>
        /// The location of the resource within the contents folder of the epub file.
        /// Example:<br/> images/cover.jpg<br/> content/chapter1.xhtml<br/>
        /// </summary>
        public string getHref()
        {

            return href;
        }

        /// <summary>
        /// The resources Id.  Must be both unique within all the resources of this book
        /// and a valid identifier.
        /// </summary>
        public string getId()
        {

            return id;
        }

        /// <summary>
        /// The character encoding of the resource. Is allowed to be null for non-text
        /// resources like images.
        /// </summary>
        public string getInputEncoding()
        {
            return inputEncoding;
        }

        /// <summary>
        /// Gets the contents of the Resource as an InputStream.
        /// </summary>
        /// The contents of the Resource.
        public System.IO.Stream getInputStream()
        {
            Stream stream = new MemoryStream(data);
            return stream;
        }

        /// <summary>
        /// This resource's mediaType.
        /// </summary>
        public MediaType getMediaType()
        {
            return mediaType;
        }


        /// <summary>
        /// Returns the size of this resource in bytes.
        /// </summary>
        /// the size.
        public long getSize()
        {
            if (data != null)
            {
                return data.Length;
            }

            return cachedSize;
        }

        /// <summary>
        /// If the title is found by scanning the underlying html document then it is
        /// cached here.
        /// </summary>
        public string getTitle()
        {
            return title;
        }

        /// <summary>
        /// Gets the hashCode of the Resource's href.
        /// </summary>
        public int hashCode()
        {
            return href.GetHashCode();
        }

        /// <summary>
        /// Returns if the data for this resource has been loaded into memory.
        /// </summary>
        /// true if data was loaded.
        public bool isInitialized()
        {
            return this.data != null;
        }

        /// <summary>
        /// Sets the data of the Resource. If the data is a of a different type then the
        /// original data then make sure to change the MediaType.
        /// </summary>
        /// <param name="data">data</param>
        public void setData(byte[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Sets the Resource's href.
        /// </summary>
        /// <param name="href">href</param>
        public void setHref(string href)
        {
            this.href = href;
        }

        /// <summary>
        /// Sets the Resource's id: Make sure it is unique and a valid identifier.
        /// </summary>
        /// <param name="id">id</param>
        public void setId(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// Sets the Resource's input character encoding.
        /// </summary>
        /// <param name="encoding">encoding</param>
        public void setInputEncoding(string encoding)
        {
            this.inputEncoding = encoding;
        }

        /// 
        /// <param name="mediaType"></param>
        public void setMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;
        }

        /// 
        /// <param name="title"></param>
        public void setTitle(string title)
        {
            this.title = title;
        }

        public string toString()
        {
            return string.Format("[\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"]", title, inputEncoding, mediaType.getName(), href,
                                 (data == null ? 0 : data.Length));
        }

    }//end Resource

}//end namespace domain