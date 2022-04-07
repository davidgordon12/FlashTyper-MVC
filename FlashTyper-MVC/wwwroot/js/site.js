var timerLabel = document.getElementById('timer');
var wordsToType = document.getElementById('wordsToType');
var seconds = 20;
var running = false;

let input = document.getElementById('txtInput');
input.onpaste = e => e.preventDefault();

function update() {
    if (!running) {
        running = true;
        const timer = setInterval(function () {
            timerLabel.textContent = seconds--;

            if (seconds == -1) {
                clearInterval(timer);
                seconds = 20;

                running = false;

                document.getElementById("gameForm").submit();
            }

        }, 1000);
    }
}



function reset() {
    seconds = 20;
    timerLabel.textContent = seconds;

    loadWords();
}

function loadWords() {
    wordsToType.value = null

    for (let i = 0; i < 50; i++)
    {
        wordsToType.value += words[Math.floor(Math.random() * 99)] + " ";
    }

}

function gameOver()
{
    
}

const words =
    [
        "twilight", "role", "shark", "doctor", "captivate", "boy", "fool",
        "physics", "pace", "resource", "virus", "strict", "register", "participate",
        "pleasure", "spin", "favorable", "skin", "technique", "ignite", "appetite",
        "collar", "venture", "alive", "boom", "groan", "direct", "plot", "perfume",
        "attention", "beer", "innovation", "preoccupation", "unlikely", "cultural",
        "variation", "album", "fur", "efflux", "appear", "biography", "dismissal",
        "congress", "invasion", "europe", "bland", "equal", "unity", "sport", "ethics",
        "nap", "mass", "feedback", "terrify", "so", "burst",
        "thanks", "rung", "necklace", "can", "laboratory", "tolerant",
        "exposure", "install", "size", "winner", "loud", "fever",
        "meadow", "wrestle", "berry", "pursuit", "extinct", "kettle",
        "adjust", "subway", "coalition", "terrace", "evaluate", "swarm",
        "improvement", "throat", "west", "cry", "pool", "college", "examination",
        "harvest", "poetry", "approach", "network", "diplomatic", "safari",
        "tank", "do", "dine", "wife", "lobby", "registration", "divinity"
    ]
