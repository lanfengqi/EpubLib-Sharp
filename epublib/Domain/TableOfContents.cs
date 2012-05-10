///////////////////////////////////////////////////////////
//  TableOfContents.cs
//  Implementation of the Class TableOfContents
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:07
//  Original author: lanfengqi
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using nl.siegmann.epublib.domain;
namespace nl.siegmann.epublib.domain
{
    /// <summary>
    /// The table of contents of the book. The TableOfContents is a tree structure at
    /// the root it is a list of TOCReferences, each if which may have as children
    /// another list of TOCReferences.  The table of contents is used by epub as a
    /// quick index to chapters and sections within chapters. It may contain duplicate
    /// entries, may decide to point not to certain chapters, etc.  See the spine for
    /// the complete list of sections in the order in which they should be read.
    /// </summary>
    /// <see>nl.siegmann.epublib.domain.Spine</see>
    [Serializable]
    public class TableOfContents
    {

        private static string DEFAULT_PATH_SEPARATOR = "/";
        private static readonly long serialVersionUID = -3147391239966275152L;
        private List<TOCReference> tocReferences;

        public TableOfContents()
            : this(new List<TOCReference>())
        {

        }

        /// 
        /// <param name="tocReferences"></param>
        public TableOfContents(List<TOCReference> tocReferences)
        {
            this.tocReferences = tocReferences;
        }

        /// <summary>
        /// Calls addTOCReferenceAtLocation after splitting the path using the
        /// DEFAULT_PATH_SEPARATOR.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="path"></param>
        public TOCReference addSection(Resource resource, string path)
        {
            return addSection(resource, path, DEFAULT_PATH_SEPARATOR);
        }

        /// <summary>
        /// Calls addTOCReferenceAtLocation after splitting the path using the given
        /// pathSeparator.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="path"></param>
        /// <param name="pathSeparator"></param>
        public TOCReference addSection(Resource resource, string path, string pathSeparator)
        {
            String[] pathElements = path.Split(Convert.ToChar(pathSeparator));
            return addSection(resource, pathElements);
        }

        /// <summary>
        /// Adds the given Resources to the TableOfContents at the location specified by
        /// the pathElements.  Example: Calling this method with a Resource and new
        /// String[] {"chapter1", "paragraph1"} will result in the following:
        ///            <ul>
        ///            <li>a TOCReference with the title "chapter1" at the root level.<br/>
        /// If this TOCReference did not yet exist it will have been created and does not
        /// point to any resource</li>
        ///            <li>A TOCReference that has the title "paragraph1". This
        /// TOCReference will be the child of TOCReference "chapter1" and will point to the
        /// given Resource</li>
        ///            </ul>
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="pathElements"></param>
        public TOCReference addSection(Resource resource, String[] pathElements)
        {
            if (pathElements == null || pathElements.Length == 0)
            {
                return null;
            }
            TOCReference result = null;
            List<TOCReference> currentTocReferences = this.tocReferences;
            for (int i = 0; i < pathElements.Length; i++)
            {
                String currentTitle = pathElements[i];
                result = findTocReferenceByTitle(currentTitle, currentTocReferences);
                if (result == null)
                {
                    result = new TOCReference(currentTitle, null);
                    currentTocReferences.Add(result);
                }
                currentTocReferences = result.getChildren();
            }
            result.setResource(resource);
            return result;
        }

        /// <summary>
        /// Adds the given Resources to the TableOfContents at the location specified by
        /// the pathElements.  Example: Calling this method with a Resource and new int[]
        /// {0, 0} will result in the following:
        ///            <ul>
        ///            <li>a TOCReference at the root level.<br/> If this TOCReference did
        /// not yet exist it will have been created with a title of "" and does not point
        /// to any resource</li>
        ///            <li>A TOCReference that points to the given resource and is a child
        /// of the previously created TOCReference.<br/> If this TOCReference didn't exist
        /// yet it will be created and have a title of ""</li>
        ///            </ul>
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="pathElements"></param>
        /// <param name="sectionTitlePrefix"></param>
        /// <param name="sectionNumberSeparator"></param>
        public TOCReference addSection(Resource resource, int[] pathElements, string sectionTitlePrefix, string sectionNumberSeparator)
        {
            if (pathElements == null || pathElements.Length == 0)
            {
                return null;
            }
            TOCReference result = null;
            List<TOCReference> currentTocReferences = this.tocReferences;
            for (int i = 0; i < pathElements.Length; i++)
            {
                int currentIndex = pathElements[i];
                if (currentIndex > 0 && currentIndex < (currentTocReferences.Count - 1))
                {
                    result = currentTocReferences[currentIndex];
                }
                else
                {
                    result = null;
                }
                if (result == null)
                {
                    paddTOCReferences(currentTocReferences, pathElements, i, sectionTitlePrefix, sectionNumberSeparator);
                    result = currentTocReferences[currentIndex];
                }
                currentTocReferences = result.getChildren();
            }
            result.setResource(resource);
            return result;
        }

        /// 
        /// <param name="tocReference"></param>
        public TOCReference addTOCReference(TOCReference tocReference)
        {
            if (tocReferences == null)
            {
                tocReferences = new List<TOCReference>();
            }
            tocReferences.Add(tocReference);
            return tocReference;
        }

        public int calculateDepth()
        {
            return calculateDepth(tocReferences, 0);
        }

        /// 
        /// <param name="tocReferences"></param>
        /// <param name="currentDepth"></param>
        private int calculateDepth(List<TOCReference> tocReferences, int currentDepth)
        {
            int maxChildDepth = 0;
            foreach (TOCReference tocReference in tocReferences)
            {
                int childDepth = calculateDepth(tocReference.getChildren(), 1);
                if (childDepth > maxChildDepth)
                {
                    maxChildDepth = childDepth;
                }
            }
            return currentDepth + maxChildDepth;
        }

        /// 
        /// <param name="pathElements"></param>
        /// <param name="pathPos"></param>
        /// <param name="lastPos"></param>
        /// <param name="sectionPrefix"></param>
        /// <param name="sectionNumberSeparator"></param>
        private string createSectionTitle(int[] pathElements, int pathPos, int lastPos, string sectionPrefix, string sectionNumberSeparator)
        {
            StringBuilder title = new StringBuilder(sectionPrefix);
            for (int i = 0; i < pathPos; i++)
            {
                if (i > 0)
                {
                    title.Append(sectionNumberSeparator);
                }
                title.Append(pathElements[i] + 1);
            }
            if (pathPos > 0)
            {
                title.Append(sectionNumberSeparator);
            }
            title.Append(lastPos + 1);
            return title.ToString();
        }


        /// <summary>
        /// Finds the first TOCReference in the given list that has the same title as the
        /// given Title.
        /// </summary>
        /// null if not found.
        /// <param name="title"></param>
        /// <param name="tocReferences"></param>
        private static TOCReference findTocReferenceByTitle(string title, List<TOCReference> tocReferences)
        {
            foreach (TOCReference tocReference in tocReferences)
            {
                if (title.Equals(tocReference.getTitle()))
                {
                    return tocReference;
                }
            }
            return null;
        }

        /// <summary>
        /// All unique references (unique by href) in the order in which they are
        /// referenced to in the table of contents.
        /// </summary>
        public List<Resource> getAllUniqueResources()
        {
            List<String> uniqueHrefs = new List<String>();
            List<Resource> result = new List<Resource>();
            getAllUniqueResources(uniqueHrefs, result, tocReferences);
            return result;
        }

        /// 
        /// <param name="uniqueHrefs"></param>
        /// <param name="result"></param>
        /// <param name="tocReferences"></param>
        private static void getAllUniqueResources(List<String> uniqueHrefs, List<Resource> result, List<TOCReference> tocReferences)
        {
            foreach (TOCReference tocReference in tocReferences)
            {
                Resource resource = tocReference.getResource();
                if (resource != null && !uniqueHrefs.Contains(resource.getHref()))
                {
                    uniqueHrefs.Add(resource.getHref());
                    result.Add(resource);
                }
                getAllUniqueResources(uniqueHrefs, result, tocReference.getChildren());
            }
        }

        public List<TOCReference> getTocReferences()
        {
            return tocReferences;
        }

        /// 
        /// <param name="tocReferences"></param>
        private static int getTotalSize(List<TOCReference> tocReferences)
        {
            int result = tocReferences.Count;
            foreach (TOCReference tocReference in tocReferences)
            {
                result += getTotalSize(tocReference.getChildren());
            }
            return result;
        }

        /// 
        /// <param name="currentTocReferences"></param>
        /// <param name="pathElements"></param>
        /// <param name="pathPos"></param>
        /// <param name="sectionPrefix"></param>
        /// <param name="sectionNumberSeparator"></param>
        private void paddTOCReferences(List<TOCReference> currentTocReferences, int[] pathElements, int pathPos, string sectionPrefix, string sectionNumberSeparator)
        {
            for (int i = currentTocReferences.Count; i <= pathElements[pathPos]; i++)
            {
                String sectionTitle = createSectionTitle(pathElements, pathPos, i, sectionPrefix,
                        sectionNumberSeparator);
                currentTocReferences.Add(new TOCReference(sectionTitle, null));
            }
        }

        /// 
        /// <param name="tocReferences"></param>
        public void setTocReferences(List<TOCReference> tocReferences)
        {
            this.tocReferences = tocReferences;
        }

        /// <summary>
        /// The total number of references in this table of contents.
        /// </summary>
        public int size()
        {
           return getTotalSize(tocReferences);
        }

    }//end TableOfContents

}//end namespace domain