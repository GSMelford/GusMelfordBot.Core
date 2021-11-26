let playerPath = "player.html";
let baseUrl = document.location.href.replace(playerPath, "");
let isVideoChanging = false;

async function start() {
    document.addEventListener("keyup", keyDownHandler);
    await executeRequest(baseUrl + "player/start");
    createElementVideoInfo();
}

async function keyDownHandler(event) {
    if(!isVideoChanging){
        isVideoChanging = true;
        
        if (event.key === "x" || event.key === "ArrowRight" || event.key === "X") {
            await changeVideo("player/video/new/next");
        }
        else if (event.key === "z" || event.key === "ArrowLeft" || event.key === "Z") {
            await changeVideo("player/video/new/prev");
        }

        isVideoChanging = false;
    }
}

async function changeVideo(methodName) {
    let videoBlocks = document.getElementsByClassName("video-block");
    for (let i = 0; i < videoBlocks.length; i++) {
        videoBlocks[i].remove();
    }

    createElementVideoInfo(await executeRequest(baseUrl + methodName));
    let video = createElementVideo();
    let source = createElementVideoSource("player/video/current");
    
    let videoContainer = document.getElementById("video-container");
    video.appendChild(source);
    videoContainer.appendChild(video);
    document.body.requestFullscreen().then(r => r);
}

function createElementVideo(){
    let video = document.createElement("video");
    video.setAttribute("id", "video-block");
    video.setAttribute("class", "video-block");
    video.setAttribute("name", "media");
    video.setAttribute("controls", "controls");
    video.setAttribute("autoplay", "autoplay");
    video.setAttribute("loop", "loop");
    return video;
}

function createElementVideoSource(methodName){
    let source = document.createElement("source");
    source.setAttribute("src", baseUrl + methodName);
    source.setAttribute("type", "video/mp4");
    return source;
}

function createElementVideoInfo(info){
    let div = document.getElementById("video-info");
    if(div !== null){
        div.remove();
    }
    
    div = document.createElement("div");
    div.setAttribute("id", "video-info");

    for (let prop in info) {
        console.log("info." + prop + " = " + info[prop]);
        let p = document.createElement("p");
        p.innerText = info[prop];
        div.appendChild(p);
    }
    
    let infoContainer = document.getElementById("info-container");
    infoContainer.appendChild(div);
}

async function executeRequest(requestUrl) {
    let response = await fetch(requestUrl);
    return await response.json();
}