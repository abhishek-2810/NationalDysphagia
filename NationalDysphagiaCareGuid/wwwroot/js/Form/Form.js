let userData = {}
let userHistory = {}
let userCurrentCondition = {}
const baseUrl = window.location.origin

let step = document.getElementsByClassName('step');
let prevBtn = document.getElementById('prev-btn');
let nextBtn = document.getElementById('next-btn');
let submitBtn = document.getElementById('submit-btn');
let form = document.getElementsByTagName('form')[0];
let preloader = document.getElementById('preloader-wrapper');
let bodyElement = document.querySelector('body');
let succcessDiv = document.getElementById('success');

let current_step = 0;
let stepCount = 26
step[current_step].classList.add('d-block');
if (current_step == 0) {
    prevBtn.classList.add('d-none');
    submitBtn.classList.add('d-none');
    nextBtn.classList.add('d-inline-block');
}

const progress = (value) => {
    document.getElementsByClassName('progress-bar')[0].style.width = `${value}%`;
}

nextBtn.addEventListener('click', () => {
    current_step++;
    let previous_step = current_step - 1;
    if ((current_step > 0) && (current_step <= stepCount)) {
        prevBtn.classList.remove('d-none');
        prevBtn.classList.add('d-inline-block');
        step[current_step].classList.remove('d-none');
        step[current_step].classList.add('d-block');
        step[previous_step].classList.remove('d-block');
        step[previous_step].classList.add('d-none');
        if (current_step == stepCount) {
            submitBtn.classList.remove('d-none');
            submitBtn.classList.add('d-inline-block');
            nextBtn.classList.remove('d-inline-block');
            nextBtn.classList.add('d-none');
        }
    } else {
        if (current_step > stepCount) {
            form.onsubmit = () => {
                return true
            }
        }
    }
    progress((100 / stepCount) * current_step);
});

prevBtn.addEventListener('click', () => {
    if (current_step > 0) {
        current_step--;
        let previous_step = current_step + 1;
        prevBtn.classList.add('d-none');
        prevBtn.classList.add('d-inline-block');
        step[current_step].classList.remove('d-none');
        step[current_step].classList.add('d-block')
        step[previous_step].classList.remove('d-block');
        step[previous_step].classList.add('d-none');
        if (current_step < stepCount) {
            submitBtn.classList.remove('d-inline-block');
            submitBtn.classList.add('d-none');
            nextBtn.classList.remove('d-none');
            nextBtn.classList.add('d-inline-block');
            prevBtn.classList.remove('d-none');
            prevBtn.classList.add('d-inline-block');
        }
    }

    if (current_step == 0) {
        prevBtn.classList.remove('d-inline-block');
        prevBtn.classList.add('d-none');
    }
    progress((100 / stepCount) * current_step);
});

submitBtn.addEventListener('click', () => {
    preloader.classList.add('d-block');

    const timer = ms => new Promise(res => setTimeout(res, ms));

    timer(3000)
        .then(() => {
            bodyElement.classList.add('loaded');
        }).then(() => {
            step[stepCount].classList.remove('d-block');
            step[stepCount].classList.add('d-none');
            prevBtn.classList.remove('d-inline-block');
            prevBtn.classList.add('d-none');
            submitBtn.classList.remove('d-inline-block');
            submitBtn.classList.add('d-none');
            succcessDiv.classList.remove('d-none');
            succcessDiv.classList.add('d-block');
        })

});

$(document).ready(function () {
    $("form#dysphagia-user-assessment-form").submit(function () {
        event.preventDefault()

        userCurrentCondition.PainWhileSwallowing = $("input[name=\"pain_swallowing\"]:checked").val() ?? 0
        userCurrentCondition.UnableToSwallowTheMedication = $("input[name=\"unable_swallow\"]:checked").val() ?? 0
        userCurrentCondition.CoughingChokingWhileEating = $("input[name=\"cough_choke_eating\"]:checked").val() ?? 0
        userCurrentCondition.RespiratoryDistressWhileEating = $("input[name=\"breath_difficulty_eating\"]:checked").val() ?? 0
        userCurrentCondition.HoldingFoodInMouthForLongTime = $("input[name=\"food_mouth_long\"]:checked").val() ?? 0
        userCurrentCondition.DroolingWhileEating = $("input[name=\"drool_eating\"]:checked").val() ?? 0
        userCurrentCondition.FeelingLikeFoodStuckedInThroat = $("input[name=\"food_stuck_throat\"]:checked").val() ?? 0
        userCurrentCondition.FeelingVomitingAfterEating = $("input[name=\"nausea_vomit_eating\"]:checked").val() ?? 0
        userCurrentCondition.FeelingChestBurningAfterEating = $("input[name=\"burning_sensation_chest\"]:checked").val() ?? 0
        userCurrentCondition.FeelingFoodStuckingInChest = $("input[name=\"food_stuck_in_chest\"]:checked").val() ?? 0
        userCurrentCondition.Other = $("#other_current_condition").val() ?? 0

        userHistory.Dysphagia = $("input[name=\"dysphagia_history\"]:checked").val() ?? 0
        userHistory.RespiratoryIssue = $("input[name=\"aspiration_respiratory_history\"]:checked").val() ?? 0
        userHistory.Stroke = $("input[name=\"stroke_history\"]:checked").val() ?? 0
        userHistory.Cancer = $("input[name=\"head_neck_cancer_history\"]:checked").val() ?? 0
        userHistory.GastrologicalIssue = $("input[name=\"gastrological_history\"]:checked").val() ?? 0
        userHistory.SurgeriesInPast = $("input[name=\"surgery_history\"]:checked").val() ?? 0
        userHistory.FeedingMode = $("input[name=\"current_feeding_mode\"]:checked").val() ?? ""
        userHistory.CompromisingDiet = $("input[name=\"compromising_diet\"]:checked").val() ?? 0
        userHistory.PsychologicalIssues = $("input[name=\"psychological_history\"]:checked").val() ?? 0
        userHistory.Other = $("#other_issues_history").val() ?? ""

        userData.FirstName = $("#first_name").val()
        userData.LastName = $("#last_name").val()
        userData.Gender = $("input[name=\"gender\"]:checked").val()
        userData.Age = $("#age").val()
        userData.Email = $("#email").val()
        userData.PhoneNumber = $("#phone").val()

        $.ajax({
            url: baseUrl + "/api/Patients",
            type: "POST",
            data: JSON.stringify(userData),
            contentType: "application/json",
            success: function (responseData, textStatus, jqXHR) {
                userData.PatientId = responseData.PatientId
                userHistory.Patient = userData.PatientId

                $.ajax({
                    url: baseUrl + "/api/PatientHistories",
                    type: "POST",
                    data: JSON.stringify(userHistory),
                    contentType: "application/json",
                    success: function (responseData, textStatus, jqXHR) {
                        userCurrentCondition.Patient = userData.PatientId

                        $.ajax({
                            url: baseUrl + "/api/PatientCurrentStates",
                            type: "POST",
                            data: JSON.stringify(userCurrentCondition),
                            contentType: "application/json",
                            success: function (responseData, textStatus, jqXHR) {
                                console.log("Form Submitted Successfully! ID: " + userData.PatientId)

                                postFormSubmissionSteps(userData.PatientId)
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.log(errorThrown);
                            }
                        });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    });
});

function postFormSubmissionSteps(id) {
    $.ajax({
        url: baseUrl + "/form/PostFormSubmission",
        type: "POST",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (responseData, textStatus, jqXHR) {
            console.log("Email Sent!")
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}