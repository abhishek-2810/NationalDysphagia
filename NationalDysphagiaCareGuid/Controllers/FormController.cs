using Microsoft.AspNetCore.Mvc;
using NationalDysphagiaCareGuid.Models;
using System.Net.Mail;
using System.Net;

namespace NationalDysphagiaCareGuid.Controllers
{
    public class FormController : Controller
    {
        private readonly NationalDysphagiaCareGuidDbContext _context;

        public FormController(NationalDysphagiaCareGuidDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostFormSubmission([FromBody]int id)
        {
            PatientsController patients=new PatientsController(_context);
            var patient = patients.GetPatient(id);

            try
            {
                if (patient.IsCompletedSuccessfully && patient.Result.Value != null)
                {
                    SendEmail(patient.Result.Value);
                }

                return Json(new { Status = true });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, ex.Message });
            }
        }

        private bool SendEmail(Patient patient)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("28a.v10@gmail.com", "ekzuwuhthgkhrmbz"),
                    EnableSsl = true,
                };

                string convertToYesNo(int value)
                {
                    return value == 1 ? "YES" : "NO";
                }

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("28a.v10@gmail.com"),
                    Subject = $"Nation Dysphagia Connecting Guide - New Patient Form - {patient.FirstName} {patient.LastName} (Patient ID: {patient.PatientId})",
                    Body = $@"<!DOCTYPE html>
                    <html lang=\""en\"">
                       <head>
                          <meta charset=\""UTF-8\"">
                          <meta name=\""viewport\"" content=\""width=device-width,initial-scale=1\"">
                          <title>Patient Details</title>
                          <style>body{{font-family:Arial,sans-serif;line-height:1.6;margin:0;padding:20px}}table{{width:100%;border-collapse:collapse;margin-bottom:20px}}td,th{{border:1px solid #ddd;padding:8px;text-align:left}}th{{background-color:#f2f2f2}}h2{{color:#333}}</style>
                       </head>
                       <body>
                          <h2>Patient Details</h2>
                          <p>Dear [Recipient's Name],</p>
                          <p>Here are the details of the patient you requested:</p>
                          <table>
                             <tr>
                                <th>Attribute</th>
                                <th>Details</th>
                             </tr>
                             <tr>
                                <td>Patient ID</td>
                                <td>{patient.PatientId}</td>
                             </tr>
                             <tr>
                                <td>First Name</td>
                                <td>{patient.FirstName}</td>
                             </tr>
                             <tr>
                                <td>Last Name</td>
                                <td>{patient.LastName}</td>
                             </tr>
                             <tr>
                                <td>Gender</td>
                                <td>{patient.Gender}</td>
                             </tr>
                             <tr>
                                <td>Age</td>
                                <td>{patient.Age}</td>
                             </tr>
                             <tr>
                                <td>Email</td>
                                <td>{patient.Email}</td>
                             </tr>
                             <tr>
                                <td>Phone Number</td>
                                <td>{patient.PhoneNumber}</td>
                             </tr>
                          </table>
                          <h3>Patient History:</h3>
                          <table>
                             <tr>
                                <th>Attribute</th>
                                <th>Details</th>
                             </tr>
                             <tr>
                                <td>Dysphagia</td>
                                <td>{convertToYesNo(patient.PatientHistories.First().Dysphagia)}</td>
                             </tr>
                             <tr>
                                <td>Respiratory Issue</td>
                                <td>{convertToYesNo(patient.PatientHistories.First().RespiratoryIssue)}</td>
                             </tr>
                             <tr>
                                <td>Stroke</td>
                                <td>{convertToYesNo(patient.PatientHistories.First().Stroke)}</td>
                             </tr>
                             <tr>
                                <td>Cancer</td>
                                <td>{convertToYesNo(patient.PatientHistories.First().Cancer)}</td>
                             </tr>
                             <tr>
                                <td>Gastrological Issue</td>
                                <td>{convertToYesNo(patient.PatientHistories.First().GastrologicalIssue)}</td>
                             </tr>
                             <tr>
                                <td>Surgeries in Past</td>
                                <td>{convertToYesNo(patient.PatientHistories.First().SurgeriesInPast)}</td>
                             </tr>
                             <tr>
                                <td>Feeding Mode</td>
                                <td>{patient.PatientHistories.First().FeedingMode}</td>
                             </tr>

                          </table>
                          <h3>Current State:</h3>
                          <table>
                             <tr>
                                <th>Attribute</th>
                                <th>Details</th>
                             </tr>
                             <tr>
                                <td>Pain While Swallowing</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().PainWhileSwallowing)}</td>
                             </tr>
                             <tr>
                                <td>Unable to Swallow Medication</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().UnableToSwallowTheMedication)}</td>
                             </tr>
                             <tr>
                                <td>Coughing/Choking while Eating</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().CoughingChokingWhileEating)}</td>
                             </tr>
                             <tr>
                                <td>Respiratory Distress while Eating</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().RespiratoryDistressWhileEating)}</td>
                             </tr>
                             <tr>
                                <td>Holding Food in Mouth for Long Time</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().HoldingFoodInMouthForLongTime)}</td>
                             </tr>
                             <tr>
                                <td>Drooling while Eating</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().DroolingWhileEating)}</td>
                             </tr>
                             <tr>
                                <td>Feeling Food Stuck in Throat</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().FeelingLikeFoodStuckedInThroat)}</td>
                             </tr>
                             <tr>
                                <td>Feeling Vomiting after Eating</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().FeelingVomitingAfterEating)}</td>
                             </tr>
                             <tr>
                                <td>Feeling Chest Burning after Eating</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().FeelingChestBurningAfterEating)}</td>
                             </tr>
                             <tr>
                                <td>Feeling Food Stuck in Chest</td>
                                <td>{convertToYesNo(patient.PatientCurrentStates.First().FeelingFoodStuckingInChest)}</td>
                             </tr>
                          </table>
                          <p>If you have any questions or need further information, please feel free to reach out.</p>
                          <p>Best regards,<br>[Your Name]</p>
                       </body>
                    </html>",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add("28a.v10@gmail.com");//gnikhilaks@gmail.com
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
