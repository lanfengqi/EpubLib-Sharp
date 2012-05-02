///////////////////////////////////////////////////////////
//  ResourceReference.cs
//  Implementation of the Class ResourceReference
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:21:05
//  Original author: lanfengqi
///////////////////////////////////////////////////////////


using System;
using nl.siegmann.epublib.domain;
namespace nl.siegmann.epublib.domain {
    [Serializable]
	public class ResourceReference  {

		protected Resource resource;
		private static readonly long serialVersionUID = 2596967243557743048L;

		public ResourceReference(){

		}

		~ResourceReference(){

		}

		public virtual void Dispose(){

		}

		/// 
		/// <param name="resource"></param>
		public ResourceReference(Resource resource){

		}

		public Resource getResource(){

			return null;
		}

		/// <summary>
		/// The id of the reference referred to.  null of the reference is null or has a
		/// null id itself.
		/// </summary>
		public string getResourceId(){

			return "";
		}

		/// <summary>
		/// Besides setting the resource it also sets the fragmentId to null.
		/// </summary>
		/// <param name="resource">resource</param>
		public void setResource(Resource resource){

		}

	}//end ResourceReference

}//end namespace domain