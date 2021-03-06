let homePath = "home.html";
let baseUrl = document.location.href.replace(homePath, "");

async function load(){
    let information = await executeRequest("system/info");
    let botFullName = information["systemName"] + " v" + information["systemVersion"];
    document.title = botFullName;
    
    let botTitle = document.getElementById("bot-title");
    botTitle.innerText = botFullName;

    let contentInfo = await executeRequest("app/content/info?chatId=7dfd19cb-33dc-4da6-b157-60177994b696");
    let playerButton = document.getElementById("player-button");
    playerButton.value = "View " + contentInfo.length + " contents";
}

function redirectToPlayer(){
    document.location.href = baseUrl + "apps/player.html";
}

async function executeRequest(requestUrl) {
    let response = await fetch(baseUrl + requestUrl);
    return await response.json();
}