# store

This is a test online bookshop on C# for portfolio. 

You can search the book that you are interested in, get familiar with the description and price and after that you can add it to your cart. 
When you are done shopping you can browse your cart where you can see total amount of books that you want to order and total price. 
To complete the order you have to type in your cell phone number and receive verification code (no SMS will be send due to some restrictions).
You have to specify method of delivery (at this time there is only postamate delivery) and delivery parameters such as city and street and choose payment method: cash or with your card (card payment is simulated due to some restrictions).
When order is placed you can see the summary of your order. Also the information about the order send to SQL database.

Some NuGet packages were used such as:
1. Microsoft.ApsNetCore.Http.Abstractions
2. Microsoft.EntityFrameworkCore.SqlServer
3. Microsoft.EntityFrameworkCore.Tools
4. NetStandard.Library
5. Newtonsoft.Json
6. libphonenumber -csharp
7. Microsoft.AspNetCore.Http.Extensions
