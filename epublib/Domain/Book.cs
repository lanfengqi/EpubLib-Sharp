///////////////////////////////////////////////////////////
//  Book.cs
//  Implementation of the Class Book
//  Generated by Enterprise Architect
//  Created on:      02-����-2012 16:20:58
//  Original author: lanfengqi
///////////////////////////////////////////////////////////



using System;
using System.Collections.Generic;
using nl.siegmann.epublib.domain;
namespace nl.siegmann.epublib.domain {

	public class Book  {

		private Resource coverImage;
		private Guide guide = new Guide();
		private Metadata metadata = new Metadata();
		private Resource ncxResource;
		private Resource opfResource;
		private Resources resources = new Resources();
		private static readonly long serialVersionUID = 2068355170895770100L;
		private Spine spine = new Spine();
		private TableOfContents tableOfContents = new TableOfContents();

		public Book(){

		}

		~Book(){

		}

		public virtual void Dispose(){

		}

		/// 
		/// <param name="resource"></param>
		public Resource addResource(Resource resource){

			return null;
		}

		/// <summary>
		/// Adds the resource to the table of contents of the book as a child section of
		/// the given parentSection
		/// </summary>
		/// <param name="parentSection"></param>
		/// <param name="sectionTitle"></param>
		/// <param name="resource"></param>
		public TOCReference addSection(TOCReference parentSection, string sectionTitle, Resource resource){

			return null;
		}

		/// <summary>
		/// Adds a resource to the book's set of resources, table of contents and if there
		/// is no resource with the id in the spine also adds it to the spine.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="resource"></param>
		public TOCReference addSection(string title, Resource resource){

			return null;
		}

		/// 
		/// <param name="resource"></param>
		/// <param name="allReachableResources"></param>
		private static void addToContentsResult(Resource resource, Dictionary<String, Resource> allReachableResources){

		}

		public void generateSpineFromTableOfContents(){

		}

		/// <summary>
		/// All Resources of the Book that can be reached via the Spine, the
		/// TableOfContents or the Guide.
		///            <p/> Consists of a list of "reachable" resources:
		///            <ul>
		///            <li>The coverpage</li>
		///            <li>The resources of the Spine that are not already in the
		/// result</li>
		///            <li>The resources of the Table of Contents that are not already in
		/// the result</li>
		///            <li>The resources of the Guide that are not already in the
		/// result</li>
		///            </ul> To get all html files that make up the epub file use
		/// </summary>
		/// <see>getResources().getAll()</see>
		public List<Resource> getContents(){

			return null;
		}

		/// <summary>
		/// The book's cover image.
		/// </summary>
		public Resource getCoverImage(){

			return null;
		}

		/// <summary>
		/// The book's cover page. An XHTML document containing a link to the cover image.
		/// </summary>
		public Resource getCoverPage(){

			return null;
		}

		/// <summary>
		/// The guide; contains references to special sections of the book like colophon,
		/// glossary, etc.
		/// </summary>
		public Guide getGuide(){

			return null;
		}

		/// <summary>
		/// The Book's metadata (titles, authors, etc)
		/// </summary>
		public Metadata getMetadata(){

			return null;
		}

		public Resource getNcxResource(){

			return null;
		}

		public Resource getOpfResource(){

			return null;
		}

		/// <summary>
		/// The collection of all images, chapters, sections, xhtml files, stylesheets, etc
		/// that make up the book.
		/// </summary>
		public Resources getResources(){

			return null;
		}

		/// <summary>
		/// The sections of the book that should be shown if a user reads the book from
		/// start to finish.
		/// </summary>
		public Spine getSpine(){

			return null;
		}

		/// <summary>
		/// The Table of Contents of the book.
		/// </summary>
		public TableOfContents getTableOfContents(){

			return null;
		}

		/// <summary>
		/// Gets the first non-blank title from the book's metadata.
		/// </summary>
		public string getTitle(){

			return "";
		}

		/// 
		/// <param name="coverImage"></param>
		public void setCoverImage(Resource coverImage){

		}

		/// 
		/// <param name="coverPage"></param>
		public void setCoverPage(Resource coverPage){

		}

		/// 
		/// <param name="metadata"></param>
		public void setMetadata(Metadata metadata){

		}

		/// 
		/// <param name="ncxResource"></param>
		public void setNcxResource(Resource ncxResource){

		}

		/// 
		/// <param name="opfResource"></param>
		public void setOpfResource(Resource opfResource){

		}

		/// 
		/// <param name="resources"></param>
		public void setResources(Resources resources){

		}

		/// 
		/// <param name="spine"></param>
		public void setSpine(Spine spine){

		}

		/// 
		/// <param name="tableOfContents"></param>
		public void setTableOfContents(TableOfContents tableOfContents){

		}

	}//end Book

}//end namespace domain