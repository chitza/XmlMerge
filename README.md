# Overview

This projects aims at mergin content from various XML files, based on XPath queries.

# Status

Work in progress!

## Usage scenario

Suppose we want to add a new book to the following "book store":
	
### File `bookstore.xml`

	<?xml version="1.0"?>
	<bookstore>
	
		<book category="Adventures">
		  <title lang="en">Treasure Island</title>
		  <author>Robert Louis Stevenson</author>
		  <year>2006</year>
		  <price>0.00</price>
		</book>
		
		<book category="Children">
		  <title lang="en">Harry Potter</title>
		  <author>J K. Rowling</author>
		  <year>2005</year>
		  <price>29.99</price>
		</book>
		
		<book category="Children">
		  <title lang="en">Alice's Adventures in Wonderland</title>
		  <author>Lewis Carroll</author>
		  <year>1997</year>
		  <price>0.99</price>
		</book>
		
	</bookstore>

### File `newbook.xml`
	
	<?xml version="1.0"?>
	<bookstore>
	
		<book category="Drama">
		  <title lang="en">Pride and Prejudice</title>
		  <author>Jane Austen</author>
		  <year>1998</year>
		  <price>9.95</price>
		</book>

	</bookstore>


	XmlMerge 
		-s[ource] newbook.xml -m "bookstore/book" 
		-t[arget] bookstore.xml -n "bookstore/book[last()]" 
		-o[utput] books_updated.xml

	
### File `books_updated.xml`

	<?xml version="1.0"?>
	<bookstore>
	
		<book category="Adventures">
		  <title lang="en">Treasure Island</title>
		  <author>Robert Louis Stevenson</author>
		  <year>2006</year>
		  <price>0.00</price>
		</book>
		
		<book category="Children">
		  <title lang="en">Harry Potter</title>
		  <author>J K. Rowling</author>
		  <year>2005</year>
		  <price>29.99</price>
		</book>
		
		<book category="Children">
		  <title lang="en">Alice's Adventures in Wonderland</title>
		  <author>Lewis Carroll</author>
		  <year>1997</year>
		  <price>0.99</price>
		</book>
		
		<book category="Drama">
		  <title lang="en">Pride and Prejudice</title>
		  <author>Jane Austen</author>
		  <year>1998</year>
		  <price>9.95</price>
		</book>
		
	</bookstore>