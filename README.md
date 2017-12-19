# RG Support Wheel Of Fate API

This is an ASP.NET Core 2.0 WebAPI application for the WoF challenge. 

- It was built on Microsoft Visual Studio Community 2017 for Mac
- It exposes a single endpoint `[host]/WheelOfFate/Get` and expects no arguments
- It will use its own database file called `supporwof.db` located at the root of the Api project
- It has migrations in case you want to switch the database provider (in startup.cs) and run them for yourself
- Please read through the comments in the code so you can understand the assumptions I made and followed, specially regarding the business rules

## To use

- Clone the Repo
- Open the Solution
- Restore/Update Nuget packages
- Run All tests just to make sure everything is passing 
- Run the Application and make sure it's running on `http://localhost:5000` (the client is hardcoded for that URL)
- Run the client
- Click the button
- Check the users and go take a look at the API code if you haven't yet
- Be happy and think Wow, this dude is great!


### Side note on installing DOTNET CLI TOOLS for manipulating EF

Add `<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />` under item group
Install Microsoft.EntityFrameworkCore.Design through Nuget Package Manager

** run dotnet restore ** instead of letting visual studio restore the packages for you
