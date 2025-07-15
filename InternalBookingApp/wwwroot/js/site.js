$(document).ready(function () {
    $("form").validate({
        rules: {
            ResourceId: { required: true },
            StartTime: { required: true },
            EndTime: { required: true },
            BookedBy: { required: true, maxlength: 50 },
            Purpose: { required: true, maxlength: 200 }
        },
        messages: {
            ResourceId: "Please select a resource.",
            StartTime: "Please select a start time.",
            EndTime: "Please select an end time.",
            BookedBy: {
                required: "Please enter your name.",
                maxlength: "Name cannot exceed 50 characters."
            },
            Purpose: {
                required: "Please enter the purpose.",
                maxlength: "Purpose cannot exceed 200 characters."
            }
        }
    });
});