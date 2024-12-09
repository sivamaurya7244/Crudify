const userName = sessionStorage.getItem("userName");
if (userName) {
    document.getElementById("welcomeMessage").textContent = `Welcome, ${userName}!`;
} else {
    window.location.href = '/Login'; 
}