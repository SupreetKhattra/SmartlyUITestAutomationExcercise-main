# UI Test Automation with Selenium, C#, and SpecFlow

## Overview

This project aims to automate UI testing for the OrangeHRM application (https://opensource-demo.orangehrmlive.com/) using Selenium with C# and SpecFlow. The tests cover various scenarios such as navigation, employee management, sorting, and file attachments. Additionally, the project incorporates running tests within a continuous integration pipeline and across different browsers.

## Test Cases

### TC1: Verify user can navigate to Employee List page using the navigation menu

1. Click ‘PIM’
2. Click ‘Employee List’
3. Verify Employee List page is loaded

### TC2: Verify user can add a new employee in the Add Employee page

1. Navigate to Employee List page
2. Click ‘Add’ button
3. Enter the employee’s name
4. Click ‘Create Login Details’ Toggle
5. Click ‘Save’ button
6. Validate errors
7. Populate mandatory fields
8. Validate errors are gone
9. Click ‘Save’ button
10. Verify ‘Successful’ snack bar message is displayed, and employee is added to the list of employees

### TC3: Verify employee details table can be sorted by name

1. Navigate to Employee List page
2. Verify First (& Middle) Name column is sorted in ascending order by default
3. Click Descending order in Last Name column
4. Verify Last name column is sorted in descending order

### TC4: Verify attachments can be added in the Personal Details page

1. Navigate to Employee List page
2. Enter the employee’s name
3. Click Search button
4. Double click on the employee name in the search table
5. Verify the employee name same as the selected name
6. Click add button in the Attachments
7. Click Browse button attached the file
8. Click ‘Save’ button
9. Verify the file name is displayed in the Attachment table
10. Delete the attached file
11. Verify the file name is not displayed in the Attachment table

## Project Structure

The project structure in Visual Studio Code is organized as follows:

- `Features`: Contains SpecFlow feature files defining test scenarios.
- `Step Definitions`: Holds the step definitions corresponding to the feature files.
- `Pages`: Includes classes with methods used in step definitions for interacting with web elements.
- `Base Class`: Contains reusable methods shared across different tests.
- `Driver Class`: Initializes and manages Chrome and Edge drivers for testing.

## Additional functionality

1. Runs tests within a continuous integration pipeline.
2. Runs tests in different browsers.


