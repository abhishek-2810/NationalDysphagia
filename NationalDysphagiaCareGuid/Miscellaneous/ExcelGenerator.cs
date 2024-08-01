using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using NationalDysphagiaCareGuid.Models;
using System.ComponentModel;
using System.Reflection;

namespace NationalDysphagiaCareGuid.Miscellaneous
{
    public class ExcelGenerator
    {
        private readonly NationalDysphagiaCareGuidDbContext _context;

        public ExcelGenerator(NationalDysphagiaCareGuidDbContext context)
        {
            _context = context;
        }

        public MemoryStream GenerateExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                // Create Patient worksheet
                var patientWorksheet = workbook.Worksheets.Add("Patients");
                CreatePatientSheet(patientWorksheet);

                // Create PatientHistory worksheet
                var patientHistoryWorksheet = workbook.Worksheets.Add("PatientHistories");
                CreatePatientHistorySheet(patientHistoryWorksheet);

                // Create PatientCurrentState worksheet
                var patientCurrentStateWorksheet = workbook.Worksheets.Add("PatientCurrentStates");
                CreatePatientCurrentStateSheet(patientCurrentStateWorksheet);

                // Save to memory stream
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return stream;
            }
        }

        private void CreateReadableSheet(IXLWorksheet worksheet)
        {
            // Header
            worksheet.Cell(1, 1).Value = "Patient Id";
            worksheet.Cell(1, 2).Value = "Age";
            worksheet.Cell(1, 3).Value = "Email ID";
            worksheet.Cell(1, 4).Value = "First Name";
            worksheet.Cell(1, 5).Value = "Last Name";
            worksheet.Cell(1, 6).Value = "Gender";
            worksheet.Cell(1, 7).Value = "Mobile Number";
            worksheet.Cell(1, 8).Value = "Registered On";
            worksheet.Cell(1, 9).Value = "Is New Record";

            worksheet.Cell(1, 10).Value = "Any past history or current diagnosis of Head and Neck cancer?";
            worksheet.Cell(1, 11).Value = "Are you compromising with your diet/cut downing your meals?";
            worksheet.Cell(1, 12).Value = "Any past history of Dysphagia (swallowing difficulty)?";
            worksheet.Cell(1, 13).Value = "How do you currently manage the nutrition?";
            worksheet.Cell(1, 14).Value = "Any Gastrointestinal issues like GERD?";
            worksheet.Cell(1, 15).Value = "Are there any other medial issues please mention below.?";
            worksheet.Cell(1, 16).Value = "Any Psychological issues and are you under any psychological medication?";
            worksheet.Cell(1, 17).Value = "History of Aspiration or any respiratory illness present?";
            worksheet.Cell(1, 18).Value = "Any past history or current diagnosis of Brain stroke?";
            worksheet.Cell(1, 19).Value = "History of any previous head and neck surgeries/brain surgeries?";

            worksheet.Cell(1, 20).Value = "Do you frequently cough/choke while eating?";
            worksheet.Cell(1, 21).Value = "Is your food falling out from your mouth while trying to eat and unless to clear the food?";
            worksheet.Cell(1, 22).Value = "Do you feel like any burning sensation in your chest?";
            worksheet.Cell(1, 23).Value = "Do you feel like food is stucking in your middle of your chest after every swallow?";
            worksheet.Cell(1, 24).Value = "Are you feeling like food sticking in your throat and unless to swallow it down?";
            worksheet.Cell(1, 25).Value = "Do you feel vomiting sensation after eating?";
            worksheet.Cell(1, 26).Value = "Are you taking more time to swallow your food from mouth to throat?";
            worksheet.Cell(1, 27).Value = "Do you have any difficulty in swallowing medicines (tablets), solids, blended food or liquids?";
            worksheet.Cell(1, 28).Value = "Do you feel any respiratory distress (breathing difficulty) while eating?";
            worksheet.Cell(1, 29).Value = "Unable To Swallow The Medication";
            worksheet.Cell(1, 30).Value = "Other";

            // Fetch data from database
            var patients = _context.Patients.Include(p=>p.PatientHistories).Include(p=>p.PatientCurrentStates).ToList();

            for (int i = 0; i < patients.Count; i++)
            {
                var patient = patients[i];
                var row = i + 2;

                worksheet.Cell(row, 1).Value = patient.PatientId;
                worksheet.Cell(row, 2).Value = patient.Age;
                worksheet.Cell(row, 3).Value = patient.Email;
                worksheet.Cell(row, 4).Value = patient.FirstName;
                worksheet.Cell(row, 5).Value = patient.LastName;
                worksheet.Cell(row, 6).Value = patient.Gender;
                worksheet.Cell(row, 7).Value = patient.PhoneNumber;
                worksheet.Cell(row, 8).Value = patient.RegistrationDate;
                worksheet.Cell(row, 9).Value = patient.IsNew == 1 ? "Yes" : "No";

                var history = patient.PatientHistories.FirstOrDefault();
                if (history != null)
                {
                    worksheet.Cell(row, 10).Value = history.Cancer == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 11).Value = history.CompromisingDiet == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 12).Value = history.Dysphagia == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 13).Value = history.FeedingMode;
                    worksheet.Cell(row, 14).Value = history.GastrologicalIssue == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 15).Value = history.Other;
                    worksheet.Cell(row, 16).Value = history.PsychologicalIssues == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 17).Value = history.RespiratoryIssue == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 18).Value = history.Stroke == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 19).Value = history.SurgeriesInPast == 1 ? "Yes" : "No";
                }

                var state = patient.PatientCurrentStates.FirstOrDefault();
                if (state != null)
                {
                    worksheet.Cell(row, 3).Value = state.CoughingChokingWhileEating == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 4).Value = state.DroolingWhileEating == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 5).Value = state.FeelingChestBurningAfterEating == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 6).Value = state.FeelingFoodStuckingInChest == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 7).Value = state.FeelingLikeFoodStuckedInThroat == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 8).Value = state.FeelingVomitingAfterEating == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 9).Value = state.HoldingFoodInMouthForLongTime == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 10).Value = state.PainWhileSwallowing == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 11).Value = state.RespiratoryDistressWhileEating == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 12).Value = state.UnableToSwallowTheMedication == 1 ? "Yes" : "No";
                    worksheet.Cell(row, 13).Value = state.Other;
                }
            }
        }

        private void CreatePatientSheet(IXLWorksheet worksheet)
        {
            // Header
            worksheet.Cell(1, 1).Value = "patientId";
            worksheet.Cell(1, 2).Value = "age";
            worksheet.Cell(1, 3).Value = "email";
            worksheet.Cell(1, 4).Value = "firstName";
            worksheet.Cell(1, 5).Value = "lastName";
            worksheet.Cell(1, 6).Value = "gender";
            worksheet.Cell(1, 7).Value = "phoneNumber";
            worksheet.Cell(1, 8).Value = "addedOn";
            worksheet.Cell(1, 9).Value = "isNew";

            // Fetch data from database
            var patients = _context.Patients.ToList();

            for (int i = 0; i < patients.Count; i++)
            {
                var patient = patients[i];
                var row = i + 2;

                worksheet.Cell(row, 1).Value = patient.PatientId;
                worksheet.Cell(row, 2).Value = patient.Age;
                worksheet.Cell(row, 3).Value = patient.Email;
                worksheet.Cell(row, 4).Value = patient.FirstName;
                worksheet.Cell(row, 5).Value = patient.LastName;
                worksheet.Cell(row, 6).Value = patient.Gender;
                worksheet.Cell(row, 7).Value = patient.PhoneNumber;
                worksheet.Cell(row, 8).Value = patient.RegistrationDate;
                worksheet.Cell(row, 9).Value = patient.IsNew;
            }
        }

        private void CreatePatientHistorySheet(IXLWorksheet worksheet)
        {
            // Header
            worksheet.Cell(1, 1).Value = "historyId";
            worksheet.Cell(1, 2).Value = "patient";
            worksheet.Cell(1, 3).Value = "cancer";
            worksheet.Cell(1, 4).Value = "compromisingDiet";
            worksheet.Cell(1, 5).Value = "dysphagia";
            worksheet.Cell(1, 6).Value = "feedingMode";
            worksheet.Cell(1, 7).Value = "gastrologicalIssue";
            worksheet.Cell(1, 8).Value = "other";
            worksheet.Cell(1, 9).Value = "psychologicalIssues";
            worksheet.Cell(1, 10).Value = "respiratoryIssue";
            worksheet.Cell(1, 11).Value = "stroke";
            worksheet.Cell(1, 12).Value = "surgeriesInPast";

            // Fetch data from database
            var histories = _context.PatientHistories.ToList();

            for (int i = 0; i < histories.Count; i++)
            {
                var history = histories[i];
                var row = i + 2;

                worksheet.Cell(row, 1).Value = history.HistoryId;
                worksheet.Cell(row, 2).Value = history.Patient;
                worksheet.Cell(row, 3).Value = history.Cancer;
                worksheet.Cell(row, 4).Value = history.CompromisingDiet;
                worksheet.Cell(row, 5).Value = history.Dysphagia;
                worksheet.Cell(row, 6).Value = history.FeedingMode;
                worksheet.Cell(row, 7).Value = history.GastrologicalIssue;
                worksheet.Cell(row, 8).Value = history.Other;
                worksheet.Cell(row, 9).Value = history.PsychologicalIssues;
                worksheet.Cell(row, 10).Value = history.RespiratoryIssue;
                worksheet.Cell(row, 11).Value = history.Stroke;
                worksheet.Cell(row, 12).Value = history.SurgeriesInPast;
            }
        }

        private void CreatePatientCurrentStateSheet(IXLWorksheet worksheet)
        {
            // Header
            worksheet.Cell(1, 1).Value = "currentStateId";
            worksheet.Cell(1, 2).Value = "patient";
            worksheet.Cell(1, 3).Value = "coughingChokingWhileEating";
            worksheet.Cell(1, 4).Value = "droolingWhileEating";
            worksheet.Cell(1, 5).Value = "feelingChestBurningAfterEating";
            worksheet.Cell(1, 6).Value = "feelingFoodStuckingInChest";
            worksheet.Cell(1, 7).Value = "feelingLikeFoodStuckedInThroat";
            worksheet.Cell(1, 8).Value = "feelingVomitingAfterEating";
            worksheet.Cell(1, 9).Value = "holdingFoodInMouthForLongTime";
            worksheet.Cell(1, 10).Value = "painWhileSwallowing";
            worksheet.Cell(1, 11).Value = "respiratoryDistressWhileEating";
            worksheet.Cell(1, 12).Value = "unableToSwallowTheMedication";
            worksheet.Cell(1, 13).Value = "other";

            // Fetch data from database
            var currentStates = _context.PatientCurrentStates.ToList();

            for (int i = 0; i < currentStates.Count; i++)
            {
                var state = currentStates[i];
                var row = i + 2;

                worksheet.Cell(row, 1).Value = state.CurrentStateId;
                worksheet.Cell(row, 2).Value = state.Patient;
                worksheet.Cell(row, 3).Value = state.CoughingChokingWhileEating;
                worksheet.Cell(row, 4).Value = state.DroolingWhileEating;
                worksheet.Cell(row, 5).Value = state.FeelingChestBurningAfterEating;
                worksheet.Cell(row, 6).Value = state.FeelingFoodStuckingInChest;
                worksheet.Cell(row, 7).Value = state.FeelingLikeFoodStuckedInThroat;
                worksheet.Cell(row, 8).Value = state.FeelingVomitingAfterEating;
                worksheet.Cell(row, 9).Value = state.HoldingFoodInMouthForLongTime;
                worksheet.Cell(row, 10).Value = state.PainWhileSwallowing;
                worksheet.Cell(row, 11).Value = state.RespiratoryDistressWhileEating;
                worksheet.Cell(row, 12).Value = state.UnableToSwallowTheMedication;
                worksheet.Cell(row, 13).Value = state.Other;
            }
        }

        private void CreateHeaders(IXLWorksheet worksheet, Type className)
        {
            switch (className.Name)
            {
                case "Patient":
                    var patientProperties = typeof(Patient).GetProperties();

                    foreach (var property in patientProperties)
                    {
                        var displayName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;
                        worksheet.Cell(1, column++).Value = displayName;
                    }
                    break;
            }
        }

    }
}
