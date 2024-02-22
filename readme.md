# Assignment
Create a backend .NET API using this empty project (this repo) for this [frontend repository](https://github.com/EdenCoNz/recruit-react/)

In addition to providing the API for the frontend application, we want you to add a feature to call a 3rd party API for the validation.

Please note that you do not need to create the frontend code for this project unless specified, you simply need to provide the backend service that would be used for the imaginary frontend

## Spec
Sky's the limit with your implementation but there are 2 main tasks that will be assessed:

1. Input Validation Code Implementation
2. Code Structure and Architecture

### Input validation Code Implementation
- Credit card field is any number
- CVC is any number
- Expiry is any valid date
- Handle appropriately when submitted fields are invalid

### Code Structure and Architecture
- No need to implement actual 3rd party API calls, pseudocode and mocked responses should be sufficient
- At a minimum, define necessary entities and method signatures to show the use and flow of data

## Commits
Please commit frequently to communicate your thoughts while working on this assignment.

## What is valued
- Tests
- Architecture

## Duration
Use roughly 2-3 hours on this assignment. You are only expected to do work on this assignment that matches the skill level of the role you are applying for, but you are welcome to do as much as you like. You are not necessarily expected to do everything in this assignment because of the short time duration of the assignment.


# Tools & Tech
You can use any tools, plugins and technologies as required for you to complete this assignment. We expect that you in this assignment demonstrate competencies with the following technologies and concepts:

-	C# .NET Framework
-	REST API
-	Classes, Interfaces and Inheritance

# Submitting Assignment
Feel free to create a public GitHub repo or private GitHub repo where it's accessible to the assessor

# Assignment results
Added new payment controller with  ProcessPayment action method.

Processpayment accept PaymentRequest as parameter(paymentrequest is class object which is having CardNumber, cvv,expirydate and amount as entities)

processpayment returns paymentreponse object(Success,TransactionId,ErrorMessage)

 Controller call stripe service class for transaction(PaymentProcessService)

 Added dummy stripe key in webconfig
 
 paymentprocessservice class has ProcessPaymentAsync method which accepts PaymentRequest class as parameter

 payment process starts by creating paymentToken, on success it return tokenid
 
 toekn id, passes to create customer  and charge the customer.

 this method returns sucess message on successful payment process
 
Onsuccessful verification, it returns transcationid along with success value as true

onfailed verfication, it returns error message  along with success value as false 

#Url to Run API(when it runs in local)

url to run the api is: https://localhost:44334/api/Payment , post request

# Successful scenario test data

Sample Data to above post call. Pass below request in body

payment request 
{
"CardNumber":1234567890123456,
"ExpiryDate":"1/24" ,
"Cvv" :"123", 
"Amount":12.0 
}

Payment response:
{
    "Success": true,
    "TransactionId": "66a85e20-302e-46bd-b7c4-01080f8893e4",
    "ErrorMessage": null
}

# Failed scenario test data

Sample Data to above post call(Month is wrong) ,

 payment request 
{
"CardNumber":1234567890123456,
"ExpiryDate":"15/24" ,
"Cvv" :"123", 
"Amount":12.0 
}

Payment reponse
{
    "Success": false,
    "TransactionId": null,
    "ErrorMessage": "Invalid payment request."
}
Image for failed scenario
![image](https://github.com/swathigoli/recruit-dotnetframework-master/assets/40425447/2472582a-b843-4302-8d58-04317d4be75a)


