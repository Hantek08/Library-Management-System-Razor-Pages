

** This is a short documentation of where to find functions and objects in the Library project. **


Pages:
In the 'Pages' folder are the Razor Pages comprised of front end code files (HTML, CSS, Javascript). Methods in the Service folder 
are called to get any necessary database data.

Service:
In the 'Service' folder all the methods are found which query the database. The services are Create, Get, Update, Delete and a Report.
Each of these classes are found in their own respective folders, containing related service methods.

Models:
The 'Models' folder contains all the static classes with properties used to create all objects needed when querying the database.
**Note: Data annotations are added manually to the .csproj file. If any errors occur with data annotations, 
please check that System.ComponentModel.DataAnnotations is included in the .csproj file.

Methods:
The 'Methods' folder holds methods that are used to handle errors related to strings being sent to the database.

Constants:
The constants folder contains the connection string used to connect to the database.


History related regions:
Everything history related relates to the history table in the database. It is used to save a history for loans, returns, bookings etc.

Category related regions:
Category refers to categories like books, movies, events, etc.

SubCategory related regions:
SubCategory refers to categories fiction, non-fiction, etc.

Global Variables:
The global variabels can be found in the Globals class, they are used for getting information from one page to the other and keeping track of 
who is logged in, whats in the cart etc.
