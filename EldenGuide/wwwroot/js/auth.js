import { db, auth } from "./utils/firebase.js";
import { createUserWithEmailAndPassword, signInWithEmailAndPassword } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-auth.js'
import { collection, getDocs, setDoc, doc } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-firestore.js'
import { onAuthStateChanged } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-auth.js';

async function signUp(name, userEmail, password) {
  try {
    const userCredential = await createUserWithEmailAndPassword(auth, userEmail, password);
    
    // Add a new document with auto-generated ID in the 'users' collection
    await setDoc(doc(db, 'users', userCredential.user.uid), {
      username: name,
      email: userEmail,
      points: 0
    });
    document.querySelector('.signup-popup').style.display = 'none';
    const unsubscribeAuthState = onAuthStateChanged(auth, (user) => {
      if (user) {
        // User is signed in
        console.log("User is signed in: " + user.uid);

        // Redirect to the home page
        redirectToHomePage();

      } else {
        // User is signed out
        // You can handle this case if needed
      }
    });
    return userCredential.user;
  } catch (error) {
    console.error("Error signing up:", error);
    handleFailedAuth(error.code);
    return null;
  }
}


async function signIn(emailOrUsername, password) {
  console.log(auth.currentUser);
  let userCredential = null;
  let userEmail = null;
  try {
    const isEmail = /\S+@\S+\.\S+/.test(emailOrUsername);

    if (isEmail) {
      userCredential = await signInWithEmailAndPassword(auth, emailOrUsername, password).catch(function (error) {
        throw error;
      });
      console.log("Signed in successfully: " + userCredential.user);

      const unsubscribeAuthState = onAuthStateChanged(auth, (user) => {
        if (user) {
          // User is signed in
          console.log("User is signed in: " + user.uid);
  
          // Redirect to the home page
          redirectToHomePage();

        } else {
          // User is signed out
          // You can handle this case if needed
        }
      });

      return userCredential.user;
    } else {
      // Retrieve user data by username
      const querySnapshot = await getDocs(collection(db, 'users'));
      
      if (querySnapshot != null) {
        querySnapshot.forEach((doc) => {
          const userData = doc.data();
          if (userData.username === emailOrUsername) {
            console.log('User found:', userData);
            userEmail = userData.email;
            console.log('User email:', userEmail);
            }
          });
          userCredential = await signInWithEmailAndPassword(auth, userEmail, password).catch(function (error) {
            throw error;
          });
          console.log("Signed in successfully: " + userCredential.user);

          const unsubscribeAuthState = onAuthStateChanged(auth, (user) => {
            if (user) {
              // User is signed in
              console.log("User is signed in: " + user.uid);
      
              // Redirect to the home page
              redirectToHomePage();

            } else {
              // User is signed out
            }
          });

          return userCredential.user;
          
      } 
      else {
        throw new Error('User not found');
      }
    }
  } catch (error) {
    handleFailedAuth(error.code);
    console.error("Error signing in:", error);
    return null;
  }
}


function redirectToAuthPage() {
  console.log('uid:', auth.currentUser);
  if (auth.currentUser == null) {
    window.location.href = '\auth.html'; 
  }
  else {
    window.location.href = '\profile.html'; 
  }
}


function handleFailedAuth(code) {
  let message;
  switch (code) {
    case "auth/email-already-in-use":
      message = "The email address you're trying to use already exists.";
      break;
    case "auth/invalid-email":
      message = "The email address you've entered is invalid.";
      break;
    case "auth/weak-password":
      message =
        "The password you've entered is too weak. Enter a stronger password (at least 6 characters) and try again";
      break;
    case "auth/user-not-found":
      message =
        "The email address you've entered doesn't match any account. Try again or click 'Sign Up' to create a new account.";
      break;
    case "auth/wrong-password":
      message =
        "The password you've entered for the given email address is incorrect. Try again.";
      break;
    case "auth/too-many-requests":
      message =
        "You've made too many unsuccessful attempts. Try again later or reset your password.";
      break;
    case "auth/invalid-username":
      message = "Please enter a username.";
      break;
    default:
      message = "An error has occurred. Please try again later.";
  }

  createAlert(
    "danger",
    `
    <h3>Uh oh!</h3>
    <hr>
    <p class='mb-0'>${message}</p>
    `
  );
}

function createAlert(type, content) {
  const modalDiv = $(
    `<div class="modal fade" id="alertModal" tabindex="-1" aria-labelledby="alertModalLabel" aria-hidden="true"></div>`
  );

  const highestZIndex = 9999; // Maximum z-index value

  modalDiv.html(`
    <div class="modal-dialog" style="position: fixed; top: 45%; left: 50%; transform: translate(-50%, -50%); z-index: ${highestZIndex + 1}">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="alertModalLabel">An error has occurred.</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="alert alert-${type}" role="alert">
            ${content}
          </div>
        </div>
      </div>
    </div>
  `);

  modalDiv.modal('show');
}

function redirectToHomePage() {
  window.location.href = '\index.html'; 
}

export { signUp, signIn, redirectToAuthPage };
