import { db, auth } from "./utils/firebase.js";
import { doc, getDoc } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-firestore.js';
import { onAuthStateChanged } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-auth.js';

onAuthStateChanged(auth, (user) => {
    if (!user) {
        window.location.href = '\auth.html';
    } else {
        console.log("User is signed in");
    }
});

const displayUserProfile = async () => {
    try {
        var uid = await getUser(); // Wait for the Promise to resolve
        console.log(uid)

        const userRef = doc(db, 'users', uid);
        console.log(userRef);
        const userDoc = await getDoc(userRef);
        console.log(userDoc);

        if (userDoc.exists()) {
            console.log("uid exists:", uid);
            const userData = userDoc.data();
            const profileDisplay = document.getElementById('profileDisplay');
            profileDisplay.innerHTML = `
                <p><strong>Username:</strong> ${userData.username}</p>
                <p><strong>Points:</strong> ${userData.points}</p>
            `;
        } else {
            console.error('User document does not exist.');
        }
    } catch (error) {
        console.error('Error fetching user profile:', error);
    }
};

async function getUser() {
    return new Promise((resolve, reject) => {
        const unsubscribe = auth.onAuthStateChanged(user => {
            if (user) {
                resolve(user.uid);
            } else {
                reject('User not authenticated');
            }
            unsubscribe();
        });
    });
}

window.onload = () => {
    displayUserProfile();

    // Add sign-out functionality
    document.getElementById('signOut').addEventListener('click', () => {
        signOut();
    });
};


async function signOut() {
    try {
      await auth.signOut();
      console.log('uid:', auth.currentUser);
      window.location.href = '\auth.html'; 
    } catch (error) {
      handleFailedAuth(error.code);
      console.error("Error signing in:", error);
    }
  }