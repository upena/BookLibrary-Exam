# BookLibrary-Exam

DATABASE
I used SQL Server 2014, the script is also included.

WEB SITE
It was developed in Visual Studio 2017, with MVC and Entity Framework 6.0
It has three menus, one for Books (maintenance and search for Author) and other two menus for Categories and Authors
In the Categories page one, many or none Book can be added in the category.

WEB APIS
They are located in the folder ../Controllers/API/BookletController

Example to get All Books
http://localhost:37778/api/Books/GetAll

Example to get Books by category name
http://localhost:37778/api/Books/GetByCategory/Drama


Example to get Books by Author name
http://localhost:37778/api/Books/GetByAuthor/Guillermo%20del%20toro

Example to get Books by Category Id
http://localhost:37778/api/Books/GetByCategoryId/1

Example to get Books by Author Id
http://localhost:37778/api/Books/GetByAuthorId/2
