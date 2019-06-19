#GrandeGifts

this project is hosted online at https://grandegiftsryanvandenberg.azurewebsites.net

Feel free to create an account, otherwise you can login with the following credentals:
Customer:
username - test
password - Test123

Admin:
username - admin
password - Admin1

As an admin, feel free to create and edit and deactive hampers or categories at will, but please be respectful and don't do something like delete all the hampers. 

To run this locally on a windows computer, install Visual Studio and Miscrosoft Sql Server. 

Then create a database for the project and replace the connection string into the appsettings.development.json file.

then open the nuget package console and run
> update-database

go to the startup.cs file and towards the bottom, uncomment the SeedHelper.Seed() method. (note: after running it once, you need to recomment this line out to use it again.)

finally press f5 to launch the project and open it in a browser. 

Author: Ryan Vandenberg