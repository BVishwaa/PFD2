import { db, auth } from "./utils/firebase.js";
import { doc, runTransaction } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-firestore.js'
import { onAuthStateChanged } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-auth.js';


onAuthStateChanged(auth, (user) => {
    if (!user) {
        // User is not signed in, redirect to /auth.html
    window.location.href = "/auth.html";
    } else {
        // User is signed in, continue with your existing code
        
    }
});


document.getElementById("form1").onsubmit = function (event) {
    event.preventDefault();
    
    var variable = parseInt(document.querySelector('input[name="variable"]:checked').value);
    var sub = parseInt(document.querySelector('input[name="sub"]:checked').value);
    var con = parseInt(document.querySelector('input[name="con"]:checked').value);
    var ifstate = parseInt(document.querySelector('input[name="ifstate"]:checked').value);

    var result = variable + sub + con + ifstate;

    document.getElementById("grade").innerHTML = result;

    var grading = [
        { score: 0, feedback: "I don't think you studied", image: "Images/none.jpg" },
        { score: 25, feedback: "You need to spend more time. Try again", image: "Images/poor.jpg" },
        { score: 50, feedback: "I think you could do better. Try again.", image: "Images/avg.jpg" },
        { score: 75, feedback: "So close. Try again.", image: "Images/above.jpg" },
        { score: 100, feedback: "Excellent! You're a Digibank pro!", image: "Images/excellent.jpg" },
    ];

    var result2;
    for (let i = 0; i < grading.length; i++) {
        if (result == grading[i].score) {
            result2 = grading[i].feedback + "<br /><img src='" + grading[i].image + "' width='30%'  />";
        }
    }

    // document.getElementById("grade2").innerHTML = result2;

    var point = result/5;
    console.log(point);
    updatePoints(point);

    var submitButton = document.getElementById('submit');

    submitButton.disabled = true;
    submitButton.style.display = 'none';

    return false;
};

async function updatePoints(varPoints){
    console.log(auth.currentUser.uid);
    const userRef = doc(db, 'users', auth.currentUser.uid);


    runTransaction(db, transaction => {
        return transaction.get(userRef).then(userDoc => {
            if (!userDoc.exists()) {
                throw "Document does not exist!";
            }
    
            const currentPoints = userDoc.data().points;
            const newPoints = currentPoints + varPoints;
    
            transaction.update(userRef, { points: newPoints });
        });
    })
        .then(() => {
            console.log("Transaction successfully committed!");
        })
        .catch(error => {
            console.error("Transaction failed: ", error);
        });
}

export { updatePoints }
