import { initializeApp } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-app.js'

// Add Firebase products that you want to use
import { getAuth, createUserWithEmailAndPassword, signInWithEmailAndPassword } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-auth.js'
import { getFirestore } from 'https://www.gstatic.com/firebasejs/10.5.2/firebase-firestore.js'

const firebaseConfig = {
  apiKey: "AIzaSyDwtcimve44gMxA7scetqfqgNCjs2jf6Ug",
  authDomain: "elden-guide.firebaseapp.com",
  projectId: "elden-guide",
  storageBucket: "elden-guide.appspot.com",
  messagingSenderId: "279343828321",
  appId: "1:279343828321:web:e14c0995ae355ee0d88920",
  measurementId: "G-WXXMVDX748"
};

const app = initializeApp(firebaseConfig);


const db = getFirestore(app);
const auth = getAuth(app);

export { db, auth };