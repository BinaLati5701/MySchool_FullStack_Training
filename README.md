# MyApiTests

API automation framework created with:

- C#
- MSTest
- RestSharp
- Newtonsoft.Json
- Visual Studio Code
- Git Bash

## Project Overview

This project contains automated API test cases for:

https://jsonplaceholder.typicode.com/posts

The framework validates:

- Status codes
- Response structure
- JSON properties
- Data types
- Negative scenarios
- Legacy HTTP protocol validation

## Test Cases

### Positive Test Cases

1. Validate successful status code
2. Validate response returns a list of items
3. Validate response returns 100 items
4. Validate each item contains four properties
5. Validate userId property is an integer
6. Validate id property is an integer
7. Validate title property is a string
8. Validate body property is a string

### Negative Test Cases

9. Validate incorrect HTTP method returns 404 status code
10. Validate incorrect HTTP method returns empty JSON object
11. Validate incorrect endpoint returns 404 status code

### Legacy Test Cases

12. Validate endpoint is accessible through HTTP protocol

## How To Run Tests

Open terminal in Visual Studio Code and run:

```bash
dotnet test