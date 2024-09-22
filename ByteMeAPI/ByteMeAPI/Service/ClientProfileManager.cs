using ByteMeAPI.Service;
using ByteMeAPI.Service.Models.Request;
using ByteMeAPI.Service.Models.Response;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using LicenseContext = OfficeOpenXml.LicenseContext;

public class ClientProfileManager : IClientProfileManager
{
    private readonly string _filePath;

    public ClientProfileManager(string filePath)
    {
        _filePath = filePath;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Required for EPPlus usage
    }

    // Method to get personal details from the Excel file
    public ClientResponse GetAllClients(ClientRequest request)
    {
        using (var package = new ExcelPackage(new FileInfo(_filePath)))
        {
            foreach (var sheet in package.Workbook.Worksheets)
            {
                Console.WriteLine(sheet.Name);
            }
            var worksheet = package.Workbook.Worksheets["Personal Details"];
            var rowCount = worksheet?.Dimension.Rows;
        }

        return new ClientResponse();
    }

    // Method to get personal details from the Excel file
    public PersonalDetailsResponse GetClientProfile(PersonalDetailsRequest request)
    {
        using (var package = new ExcelPackage(new FileInfo(_filePath)))
        {
            foreach (var sheet in package.Workbook.Worksheets)
            {
                Console.WriteLine(sheet.Name);
            }
            var worksheet = package.Workbook.Worksheets["Personal Details"];
            var rowCount = worksheet?.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Skipping header row
            {
                int entityId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                if (entityId == request.EntityID)
                {
                    return new PersonalDetailsResponse
                    {
                        EntityID = entityId,
                        ClientCode = worksheet.Cells[row, 2].Text,
                        Company = worksheet.Cells[row, 3].Text,
                        Surname = worksheet.Cells[row, 5].Text,
                        FirstName = worksheet.Cells[row, 6].Text,
                        DateOfBirth = DateTime.Parse(worksheet.Cells[row, 7].Text),
                        // Populate other fields...
                    };
                }
            }
        }

        return null;
    }

    // Method to get compliance status
    public ComplianceStatusResponse GetComplianceStatus(ComplianceStatusRequest request)
    {
        using (var package = new ExcelPackage(new FileInfo(_filePath)))
        {
            var worksheet = package.Workbook.Worksheets["Compliance Status"];
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Skipping header row
            {
                // Assuming Entity ID is in a common column
                int entityId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                if (entityId == request.EntityID)
                {
                    return new ComplianceStatusResponse
                    {
                        Status = worksheet.Cells[row, 1].Text,
                        EffectiveDate = DateTime.Parse(worksheet.Cells[row, 2].Text),
                        ConfirmedBy = worksheet.Cells[row, 3].Text,
                        ConfirmedOn = DateTime.Parse(worksheet.Cells[row, 4].Text),
                        Months = Convert.ToInt32(worksheet.Cells[row, 5].Value)
                    };
                }
            }
        }

        return null;
    }

    // Method to get bank details
    public BankDetailsResponse GetBankDetails(int entityId)
    {
        using (var package = new ExcelPackage(new FileInfo(_filePath)))
        {
            var worksheet = package.Workbook.Worksheets["Bank Details"];
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Skipping header row
            {
                if (Convert.ToInt32(worksheet.Cells[row, 1].Value) == entityId)
                {
                    return new BankDetailsResponse
                    {
                        BankName = worksheet.Cells[row, 1].Text,
                        AccountType = worksheet.Cells[row, 2].Text,
                        AccountNumber = worksheet.Cells[row, 3].Text,
                        Branch = worksheet.Cells[row, 4].Text,
                        BranchCode = worksheet.Cells[row, 5].Text,
                        CardType = worksheet.Cells[row, 6].Text,
                        IsPrimary = worksheet.Cells[row, 7].Text == "Yes"
                    };
                }
            }
        }

        return null;
    }

    // Additional methods to pull data for other tables like ContactDetails, EmailDetails, StaffRelationships, etc.
}
