(function () {

    const alertElement = document.getElementById("success-alert");
    const formElement = document.forms[0];

    const addNewItem = async () => {
        const formData = new FormData(formElement);
        const requestData = {
            Name: formData.get("Name"),
            Description: formData.get("Description"),
            IsVisible: formData.get("IsVisible") === "true" ? true : false,
        };

        alertElement.style.display = "none";

        const response = await fetch("/api/exchanges", {
            method: "POST",
            headers: {
                "Content-type": "application/json"
            },
            body: JSON.stringify(requestData),
        });

        const responseJson = await response.json();

        if (responseJson.success) {
            alertElement.style.display = "block";
        }
    };

    window.addEventListener("load", () => {
        formElement.addEventListener("submit", event => {
            event.preventDefault();

            addNewItem().then(() => console.log("added successfully"));
        });
    });

})();