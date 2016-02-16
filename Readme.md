# WebApp
A web api project for **unusual** client requests or questions

## Project Requirements
Provide a repository of illogical tasks and unusual client requests  
Data is to be stored in a database for future reference and be retrievable (sort by date) to be displayed on a page

### Required Fields
* Persons name
* Question or Request
* Date of occurrence

## System Requirements
Built with Visual Studio 2015

### Included packages for a simple MVC Web API app
*NuGet should resolve all dependencies*

## API Calls
| API Name                       | Description                    | Request      | Response                    |
| ------------------------------ |:------------------------------:|:------------:|:---------------------------:|
|GET /api/UnusualRequest         | Gets all client requests       | None         | Collection of Request items |
|GET /api/UnusualRequest/{id}    | Gets request by ID             | None         | Request item                |
|POST /api/UnusualRequest        | Adds new Request to collection | Request item | Request item after added    |
|PUT /api/UnusualRequest/{id}    | Updates existing Request item  | Request item | None                        |
|DELETE /api/UnusualRequest/{id} | Deletes a Request item         | None         | None                        |

## Frontend Template
Basic HTML with jQuery, but as this has a Web API backend, any front end should be simple enough

## Author
S.Antonio 2016-02-15

## Notes
Very light on validation, additional libraries could have been added to assist with incorrect Data
No date picker, yyyy-MM-dd format works.
Style is plain and dull
List of data is primitive and unsortable, it is defaulted to date in the backend