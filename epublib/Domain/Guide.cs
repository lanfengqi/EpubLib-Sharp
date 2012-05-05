///////////////////////////////////////////////////////////
//  Guide.cs
//  Implementation of the Class Guide
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:00
//  Original author: lanfengqi
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using nl.siegmann.epublib.domain;
namespace nl.siegmann.epublib.domain
{
    /// <summary>
    /// The guide is a selection of special pages of the book. Examples of these are
    /// the cover, list of illustrations, etc.  It is an optional part of an epub, and
    /// support for the various types of references varies by reader.  The only part of
    /// this that is heavily used is the cover page.
    /// </summary>
    [Serializable]
    public class Guide
    {

        private static readonly int COVERPAGE_NOT_FOUND = -1;
        private static readonly int COVERPAGE_UNITIALIZED = -2;
        private int coverPageIndex = -1;
        private string DEFAULT_COVER_TITLE = GuideReference.COVER;
        private List<GuideReference> references = new List<GuideReference>();
        private static readonly long serialVersionUID = -6256645339915751189L;

        public Guide()
        {

        }

        ~Guide()
        {

        }

        public virtual void Dispose()
        {

        }

        /// 
        /// <param name="reference"></param>
        public ResourceReference addReference(GuideReference reference)
        {

            return null;
        }

        private void checkCoverPage()
        {

        }

        public string default_cover_title
        {
            get
            {
                return DEFAULT_COVER_TITLE;
            }
            set
            {
                DEFAULT_COVER_TITLE = value;
            }
        }

        /// <summary>
        /// The coverpage of the book.
        /// </summary>
        public Resource getCoverPage()
        {

            return null;
        }

        public GuideReference getCoverReference()
        {

            return null;
        }

        /// <summary>
        /// A list of all GuideReferences that have the given referenceTypeName (ignoring
        /// case).
        /// </summary>
        /// <param name="referenceTypeName"></param>
        public List<GuideReference> getGuideReferencesByType(string referenceTypeName)
        {

            return null;
        }

        public List<GuideReference> getReferences()
        {

            return null;
        }

        private void initCoverPage()
        {

        }

        /// 
        /// <param name="coverPage"></param>
        public void setCoverPage(Resource coverPage)
        {

        }

        /// 
        /// <param name="guideReference"></param>
        public int setCoverReference(GuideReference guideReference)
        {

            return 0;
        }

        /// 
        /// <param name="references"></param>
        public void setReferences(List<GuideReference> references)
        {

        }

        private void uncheckCoverPage()
        {

        }

    }//end Guide

}//end namespace domain