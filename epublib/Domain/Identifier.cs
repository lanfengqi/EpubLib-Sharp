///////////////////////////////////////////////////////////
//  Identifier.cs
//  Implementation of the Class Identifier
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:02
//  Original author: lanfengqi
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.domain
{
    /// <summary>
    /// A Book's identifier.  Defaults to a random UUID and scheme "UUID"
    /// </summary>
    [Serializable]
    public class Identifier
    {

        public static class Scheme
        {
            public static string ISBN = "ISBN";

            public static string URI = "URI";

            public static string URL = "URL";

            public static string UUID = "UUID";
        }//end Scheme

        private bool bookId = false;
        private string scheme;
        private static readonly long serialVersionUID = 955949951416391810L;
        private string value;

        /// <summary>
        /// Creates an Identifier with as value a random UUID and scheme "UUID"
        /// </summary>
        public Identifier()
            : this(Scheme.UUID, Guid.NewGuid().ToString("N"))
        {

        }

        /// 
        /// <param name="scheme"></param>
        /// <param name="value"></param>
        public Identifier(string scheme, string value)
        {
            this.scheme = scheme;
            this.value = value;
        }

        /// 
        /// <param name="otherIdentifier"></param>
        public bool equals(Object otherIdentifier)
        {
            if (!(otherIdentifier.GetType() == typeof(Identifier)))
            {
                return false;
            }
            return scheme.Equals(((Identifier)otherIdentifier).scheme)
            && value.Equals(((Identifier)otherIdentifier).value);
        }

        /// <summary>
        /// The first identifier for which the bookId is true is made the bookId identifier.
        /// If no identifier has bookId == true then the first bookId identifier is written
        /// as the primary.
        /// </summary>
        /// <param name="identifiers"></param>
        public static Identifier getBookIdIdentifier(List<Identifier> identifiers)
        {
            if (identifiers == null || identifiers.Count == 0)
            {
                return null;
            }

            Identifier result = null;
            foreach (Identifier identifier in identifiers)
            {
                if (identifier.isBookId())
                {
                    result = identifier;
                    break;
                }
            }
            if (result == null)
            {
                result = identifiers[0];
            }

            return result;
        }

        public string getScheme()
        {
            return this.scheme;
        }

        public string getValue()
        {

            return this.value;
        }

        public int hashCode()
        {
            return StringUtil.defaultIfNull(scheme).GetHashCode() ^ StringUtil.defaultIfNull(value).GetHashCode();
            return 0;
        }

        /// <summary>
        /// This bookId property allows the book creator to add multiple ids and tell the
        /// epubwriter which one to write out as the bookId.  The Dublin Core metadata spec
        /// allows multiple identifiers for a Book. The epub spec requires exactly one
        /// identifier to be marked as the book id.
        /// </summary>
        public bool isBookId()
        {
            return this.bookId;
        }

        /// 
        /// <param name="bookId"></param>
        public void setBookId(bool bookId)
        {
            this.bookId = bookId;
        }

        /// 
        /// <param name="scheme"></param>
        public void setScheme(string scheme)
        {
            this.scheme = scheme;
        }

        /// 
        /// <param name="value"></param>
        public void setValue(string value)
        {
            this.value = value;
        }

        public string toString()
        {
            if (StringUtil.isBlank(scheme))
            {
                return "" + value;
            }
            return "" + scheme + ":" + value;
        }

    }//end Identifier

}//end namespace domain