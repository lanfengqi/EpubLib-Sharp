///////////////////////////////////////////////////////////
//  TOCReference.cs
//  Implementation of the Class TOCReference
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:09
//  Original author: lanfengqi
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using nl.siegmann.epublib.domain;
namespace nl.siegmann.epublib.domain {
	/// <summary>
	/// An item in the Table of Contents.
	/// </summary>
	/// <see>nl.siegmann.epublib.domain.TableOfContents</see>
[Serializable]
	public class TOCReference : TitledResourceReference {

		private List<TOCReference> children;
        //private static readonly Comparator<TOCReference> COMPARATOR_BY_TITLE_IGNORE_CASE = new Comparator<TOCReference>() {
		           
        //                    public int compare(TOCReference tocReference1, TOCReference tocReference2) {
        //                        return String.CASE_INSENSITIVE_ORDER.compare(tocReference1.getTitle(), tocReference2.getTitle());
        //                    }
        //                };
		private static readonly long serialVersionUID = 5787958246077042456L;



		~TOCReference(){

		}

		public override void Dispose(){

		}

		public TOCReference(){

		}

		/// 
		/// <param name="name"></param>
		/// <param name="resource"></param>
		public TOCReference(string name, Resource resource){

		}

		/// 
		/// <param name="name"></param>
		/// <param name="resource"></param>
		/// <param name="fragmentId"></param>
		public TOCReference(string name, Resource resource, string fragmentId){

		}

		/// 
		/// <param name="title"></param>
		/// <param name="resource"></param>
		/// <param name="fragmentId"></param>
		/// <param name="children"></param>
		public TOCReference(string title, Resource resource, string fragmentId, List<TOCReference> children){

		}

		/// 
		/// <param name="childSection"></param>
		public TOCReference addChildSection(TOCReference childSection){

			return null;
		}

		public List<TOCReference> getChildren(){

			return null;
		}

        public static List<TOCReference> getComparatorByTitleIgnoreCase()
        {
             
			return null;
		}

		/// 
		/// <param name="children"></param>
		public void setChildren(List<TOCReference> children){

		}

	}//end TOCReference

}//end namespace domain