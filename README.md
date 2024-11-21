# Contract Monthly Claim System (CMCS)

## Table of Contents
1. [What is CMCS?](#what-is-cmcs)
2. [Purpose and Objectives](#purpose-and-objectives)
3. [Key Features](#key-features)
   - [Claim Submission](#claim-submission)
   - [Claim Verification and Approval](#claim-verification-and-approval)
   - [Status Monitoring](#status-monitoring)
   - [Administrative Management](#administrative-management)
4. [Project Overview](#project-overview)
   - [Target Audience](#target-audience)
5. [Automation](#automation)
6. [System Requirements](#system-requirements)
   - [Hardware Requirements](#hardware-requirements)
   - [Software Requirements](#software-requirements)
7. [Technology Used](#technology-used)
8. [Installation Instructions](#installation-instructions)
9. [Testing Instructions](#testing-instructions)
10. [Technical Aspects](#technical-aspects)
11. [Colour Code & Themes](#colour-code--themes)
12. [Conclusion](#conclusion)

## What is CMCS?
The Contract Monthly Claim System (CMCS) is a sophisticated web-based application specifically designed to simplify and enhance the management of monthly claims submitted by Independent Contractor (IC) lecturers. As educational institutions increasingly rely on IC lecturers to provide quality instruction, the CMCS serves as a vital tool to streamline the often complex and labor-intensive task of processing claims for services rendered.

This application not only addresses the administrative burdens associated with claim submissions but also ensures that all stakeholders have a transparent and efficient system to track and manage claims.

## Purpose and Objectives
The primary purpose of the CMCS is to enhance the submission and approval process of claims, optimizing administrative efficiency and ensuring accuracy throughout the workflow. The key objectives include:

- **Improved Efficiency**: The CMCS automates routine tasks related to claim processing, thereby reducing manual effort and the potential for human error.
  
- **Faster Claim Processing**: By streamlining the workflow, the application reduces the time taken for claims to be reviewed and approved, allowing lecturers to receive timely compensation for their services.
  
- **Precise Data Entry and Calculations**: The system ensures accurate data entry and calculations, which are critical for maintaining the integrity of the claims process and ensuring that payments are correct.
  
- **Enhanced Transparency**: The CMCS offers stakeholders visibility into the status of claim processing, from submission to final approval or rejection, fostering trust and accountability.

## Key Features

### Claim Submission
- **Intuitive Interface**: The CMCS provides a user-friendly interface that allows lecturers to submit claims with ease. This is crucial for ensuring that users of varying technical skills can navigate the system effectively.
  
- **Essential Information Capture**: The system captures vital information such as hours worked, hourly rates, as well as any other important information through an online form, ensuring all necessary data is collected upfront.
  
- **Document Upload**: Lecturers can upload supporting documents like timesheets or invoices, which are essential for substantiating their claims and ensuring comprehensive review.

### Claim Verification and Approval
- **Automated Workflows**: The CMCS introduces automated workflows that enhance the verification and approval processes, minimizing manual intervention and streamlining communication between users.
  
- **Systematic Routing**: Claims are automatically routed to Programme Coordinators and Academic Managers for processing, ensuring that each claim is reviewed by the appropriate personnel.
  
- **Decision Recording**: All decisions made regarding claims (approvals, rejections) are recorded and tracked within the system to ensure comprehensive transparency and accountability for all stakeholders involved.

### Status Monitoring
- **Real-Time Tracking**: Users can track the status of their claims in real-time, providing both lecturers and administrators with clear visibility of where each claim stands in the approval process.

### Administrative Management
- **Data Management Capabilities**: CMCS empowers Human Resources staff and administrators with a wide range of data management functionalities, including maintaining lecturer profiles and claim histories.
  
- **Seamless Database Integration**: The system can connect seamlessly with existing databases, allowing for better insights and facilitating informed decision-making based on historical data and trends.
  
- **Report Generation**: CMCS generates comprehensive reports for payment processing, auditing, and compliance, ensuring efficient administrative operations and adherence to institutional policies.

## Project Overview

### Target Audience
The CMCS caters to a diverse group of users within educational institutions, each with distinct roles and responsibilities:

- **Lecturers**: As the primary users of the system, lecturers are responsible for submitting claims, attaching supporting documents, and providing detailed information regarding their services. The system simplifies this process, ensuring timely and accurate submissions.
  
- **Programme Coordinators**: These users are tasked with scrutinizing claims to ensure accuracy and adherence to institutional standards. They verify that all claims submitted meet the necessary criteria before forwarding them for final approval.
  
- **Academic Managers**: Serving as the final approvers, Academic Managers review verified claims and make decisions based on established guidelines. Their oversight ensures that claims are managed with full accountability and a high level of scrutiny.
  
- **Human Resources Staff**: HR personnel manage the administrative tasks associated with maintaining lecturer data and processing claims. They utilize the CMCS to monitor claim histories, updates, and compliance reporting, promoting accuracy and efficiency in recordkeeping.


## Automation in CMCS

The Contract Monthly Claim System (CMCS) incorporates various automation features to streamline the claim processing workflow, reducing manual effort and enhancing accuracy. These automated processes serve to significantly improve the efficiency of lecturers, administrators, and HR personnel:

- **Claim Submission Automation:**  
  When lecturers submit their claims, the system automatically calculates the total claim amount in real-time as the lecturer enters data such as hours worked and hourly rates. This eliminates the need for lecturers to manually perform calculations, ensuring that the submitted claims are both accurate and immediately ready for review. It also helps reduce common errors associated with manual calculations, speeding up the claim submission process.

- **Claim Approval and Denial Automation for Administrators:**  
  Programme Coordinators (PC) and Academic Managers (AM) benefit from an automated claim review system that streamlines the approval process. The system automatically categorizes claims based on hours worked:
  - **Approved Claims**: Claims with hours between 0 and 15 are automatically approved.
  - **Denied Claims**: Claims with hours below zero are automatically denied.
  - **Pending Claims**: Claims with more than 15 hours are flagged as pending and require manual review to ensure accuracy and compliance.  
  This automation ensures that the approval process is faster and more consistent, while also reducing the amount of time administrators need to spend on routine claim assessments.

- **HR Report Generation Automation:**  
  HR staff have a crucial role in maintaining lecturer profiles and processing claims. To improve administrative efficiency, CMCS automates the report generation process, enabling HR to quickly generate detailed invoice reports for individual lecturers. These reports are generated with just a few clicks and include data on approved claims and the corresponding totals, reducing the time spent on manual report creation. This automated feature also ensures that HR staff can focus on more critical tasks, like compliance checks and payment processing, without having to manually compile claim histories.

By automating these key functionalities, CMCS improves the overall speed and accuracy of claim processing, reducing human error, and providing a seamless user experience for lecturers, administrators, and HR staff alike. This not only streamlines workflows but also ensures a high level of accountability, making it easier for all users to stay on top of their tasks while promoting operational efficiency.

## System Requirements

### Hardware Requirements
To ensure optimal performance of the CMCS, the following hardware specifications are recommended:
- **Processor**: 1 GHz or faster
- **RAM**: 2 GB or more
- **Hard Disk Space**: Minimum of 500 MB of free space
- **Display**: 1024 x 768 resolution or higher

### Software Requirements
The following software components are necessary for the successful deployment and operation of the CMCS:
- **Operating System**: Windows 7 or later
- **.NET Framework**: Version 4.7.2 or later (will be installed with the application)
- **IDE**: Visual Studio (for development and maintenance)
- **Database Management**: SQL Server Management Studio (SSMS)
- **Version Control**: GitHub (for source code management)
- **Framework**: .NET Core for MVC application development

## Technology Used
- **Programming Language**: C#
- **Framework**: ASP.NET Core MVC
- **Database**: SQL Server (for data management)
- **Version Control**: Git for source code management
- **Web Technologies**: HTML, CSS, JavaScript for front-end development

## Installation Instructions
To install the CMCS, follow these steps:

1. **Download the Application**: Obtain the CMCS source code from the [GitHub repository](https://github.com/VCDN-2024/prog6212-poe-part-1-ST10249843.git)
2. **Install Dependencies**:
   - Ensure that you have .NET Core SDK installed on your machine. You can download it from the [.NET website](https://dotnet.microsoft.com/download).
   - Install SQL Server Management Studio (SSMS) to manage the database.
3. **Set Up the Database**:
   - Open SSMS and create a new database named `CMCS`.
   - Execute the SQL scripts provided in the `Database` folder of the project to set up the necessary tables and relationships.
4. **Configure Application Settings**:
   - Open the `appsettings.json` file in the project and configure the database connection string to point to your newly created `CMCS` database.
5. **Build the Application**:
   - Open the solution in Visual Studio and build the application by selecting `Build > Build Solution`.
6. **Run the Application**:
   - Start the application using `Ctrl + F5` or by clicking the Start button in Visual Studio. This will launch the application in your default web browser.

## Testing Instructions
To ensure the CMCS functions as intended, follow these testing instructions:

1. **Unit Testing**:
   - Utilize a testing framework like NUnit or MSTest to create and run unit tests for individual components of the application.
   - Ensure that all core functionalities, such as claim submission, verification, and approval workflows, are covered by unit tests.

2. **Integration Testing**:
   - Test the interaction between different modules (e.g., claim submission and approval) to verify that data flows correctly through the system.
   - Perform tests on database connections and ensure that data is being accurately stored and retrieved.

3. **User Acceptance Testing (UAT)**:
   - Conduct UAT with a sample group of lecturers, programme coordinators, and academic managers to gather feedback on usability and functionality.
   - Document any issues or suggestions for improvement.

4. **Performance Testing**:
   - Test the application under load to ensure it can handle multiple simultaneous users without significant degradation in performance.
   - Use tools like JMeter or LoadRunner to simulate user interactions and measure response times.

5. **Security Testing**:
   - Perform security assessments to identify vulnerabilities in the application, including input validation, authentication, and authorization checks.
   - Utilize tools like OWASP ZAP or Burp Suite for automated security scanning.

## Technical Aspects

### Web-Based Application
The CMCS is designed as a web-based application, ensuring accessibility from any device with an internet connection. This flexibility allows users to engage with the system from various locations, enhancing usability and ensuring that critical functionalities are always available.

### Built with .NET Core MVC
Developed using the .NET Core framework with ASP.NET MVC (Model-View-Controller) architecture, CMCS provides robust performance and scalability. This structure allows for effective management of user interactions and data processing, improving both the development process and overall user experience.

![MVC](https://media.geeksforgeeks.org/wp-content/uploads/20220224160807/Model1.png)

### Colour Palette

- **Brown**: Provides grounding warmth and stability, akin to the comforting scent of leather-bound books.
- **Black**: Introduces bold sophistication, ensuring clarity and precision in navigation.
- **Green**: Symbolizes growth and renewal, highlighting key actions within the interface.
- **White**: Serves as a clean backdrop, allowing elements to stand out with elegance.

![Colour Palette](https://i.pinimg.com/originals/53/5d/d8/535dd8ae95945c465e6cdbe82927664c.png)

## Conclusion
The Contract Monthly Claim System (CMCS) is an essential tool for managing claims efficiently within educational institutions. By automating processes and enhancing transparency, CMCS not only benefits lecturers but also streamlines administrative tasks, paving the way for better resource management and improved institutional efficiency.
