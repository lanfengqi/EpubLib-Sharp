///////////////////////////////////////////////////////////
//  EpubReader.cs
//  Implementation of the Class EpubReader
//  Generated by Enterprise Architect
//  Created on:      08-����-2012 15:47:38
//  Original author: paul
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Ionic.Zip;
using nl.siegmann.epublib.epub;
using nl.siegmann.epublib.domain;
using nl.siegmann.epublib.service;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.epub
{
    /// <summary>
    /// Reads an epub file.
    /// </summary>
    public class EpubReader
    {

        private BookProcessor bookProcessor = BookProcessor.IDENTITY_BOOKPROCESSOR;
        //private static readonly Logger log = LoggerFactory.getLogger(EpubReader.class);

        /// <summary>
        /// Read epub from inputstream
        /// </summary> 
        /// <param name="stream"></param>
        public Book readEpub(Stream stream)
        {
            return readEpub(stream, Constants.ENCODING);
        }

        /// <summary>
        /// Read epub from inputstream
        /// </summary>
        /// <param name="zipInputStream"></param>
        public Book readEpub(ZipInputStream zipInputStream)
        {
            return readEpub(zipInputStream, Constants.ENCODING);
        }

        /// <summary>
        /// Read epub from inputstream
        /// </summary>
        /// <param name="stream">the inputstream from which to read the epub</param>
        /// <param name="encoding">the encoding to use for the html files within the
        /// epub</param>
        public Book readEpub(Stream stream, String encoding)
        {
            return readEpub(new ZipInputStream(stream), encoding);
        }

        /// <summary>
        /// Read epub from inputstream
        /// </summary> 
        /// <param name="zipInputStream">the inputstream from which to read the epub</param>
        /// <param name="encoding"></param>
        public Book readEpub(ZipInputStream zipInputStream, String encoding)
        {
            Book result = new Book();
            Resources resources = readResources(zipInputStream, encoding);
            handleMimeType(result, resources);
            String packageResourceHref = getPackageResourceHref(resources);
            Resource packageResource = processPackageResource(packageResourceHref, result, resources);
            result.setOpfResource(packageResource);
            Resource ncxResource = processNcxResource(packageResource, result);
            result.setNcxResource(ncxResource);
            result = postProcessBook(result);
            return result;
        }

        /// 
        /// <param name="resources"></param>
        private String getPackageResourceHref(Resources resources)
        {
            String defaultResult = "OEBPS/content.opf";
            String result = defaultResult;

            Resource containerResource = resources.remove("META-INF/container.xml");
            if (containerResource == null)
            {
                return result;
            }
            try
            {
                XElement xElement = XElement.Load(containerResource.getInputStream());
                XNamespace ns = (xElement.Attribute("xmlns") != null) ? xElement.Attribute("xmlns").Value : XNamespace.None;
                return xElement.Descendants(ns + "rootfile").FirstOrDefault((XElement p) => p.Attribute("media-type") != null && p.Attribute("media-type").Value.Equals("application/oebps-package+xml", System.StringComparison.InvariantCultureIgnoreCase)).Attribute("full-path").Value;
            }
            catch (Exception e)
            {
                //log.error(e.getMessage(), e);
            }
            if (StringUtil.isBlank(result))
            {
                result = defaultResult;
            }
            return result;
            return "";
        }

        /// 
        /// <param name="result"></param>
        /// <param name="resources"></param>
        private void handleMimeType(Book result, Resources resources)
        {
            resources.remove("mimetype");
        }

        /// 
        /// <param name="book"></param>
        private Book postProcessBook(Book book)
        {

            return null;
        }

        /// 
        /// <param name="packageResource"></param>
        /// <param name="book"></param>
        private Resource processNcxResource(Resource packageResource, Book book)
        {

            return null;
        }

        /// 
        /// <param name="packageResourceHref"></param>
        /// <param name="book"></param>
        /// <param name="resources"></param>
        private Resource processPackageResource(String packageResourceHref, Book book, Resources resources)
        {
            Resource packageResource = resources.remove(packageResourceHref);
            try
            {
                PackageDocumentReader.read(packageResource, this, book, resources);
            }
            catch (Exception e)
            {
                //log.error(e.getMessage(), e);
            }
            return packageResource;
        }



        /// <summary>
        /// Reads this EPUB without loading all resources into memory.
        /// </summary>
        /// <param name="fileName">the file to load</param>
        /// <param name="encoding">the encoding for XHTML files</param>
        /// <param name="lazyLoadedTypes">a list of the MediaType to load lazily</param>
        public Book readEpubLazy(String fileName, String encoding, List<MediaType> lazyLoadedTypes)
        {

            return null;
        }

        /// <summary>
        /// Reads this EPUB without loading any resources into memory.
        /// </summary>
        /// <param name="fileName">the file to load</param>
        /// <param name="encoding">the encoding for XHTML files</param>
        public Book readEpubLazy(String fileName, String encoding)
        {

            return null;
        }

        /// 
        /// <param name="fileName"></param>
        /// <param name="defaultHtmlEncoding"></param>
        /// <param name="lazyLoadedTypes"></param>
        private Resources readLazyResources(String fileName, String defaultHtmlEncoding, List<MediaType> lazyLoadedTypes)
        {

            return null;
        }

        /// 
        /// <param name="in"></param>
        /// <param name="defaultHtmlEncoding"></param>
        private Resources readResources(ZipInputStream zipInputStream, String defaultHtmlEncoding)
        {
            Resources result = new Resources();
            for (ZipEntry zipEntry = zipInputStream.GetNextEntry(); zipEntry != null; zipEntry = zipInputStream.GetNextEntry())
            {
                if (zipEntry.IsDirectory)
                {
                    continue;
                }
                Resource resource = ResourceUtil.createResource(zipEntry, zipInputStream);
                if (resource.getMediaType() == MediatypeService.XHTML)
                {
                    resource.setInputEncoding(defaultHtmlEncoding);
                }
                result.add(resource);
            }
            return result;
        }

    }//end EpubReader

}//end namespace epub