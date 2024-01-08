import { initializeApp } from "https://www.gstatic.com/firebasejs/10.5.2/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/10.5.2/firebase-analytics.js";
import { getDatabase } from "https://www.gstatic.com/firebasejs/10.5.2/firebase-database.js";

const firebaseConfig = {
  apiKey: "AIzaSyCx6r4IrRVybpJEax5Y6Q6bfCzJw6BA8ck",
  authDomain: "trial-ecc57.firebaseapp.com",
  databaseURL: "https://trial-ecc57-default-rtdb.firebaseio.com/",
  projectId: "trial-ecc57",
  storageBucket: "trial-ecc57.appspot.com",
  messagingSenderId: "451729452961",
  appId: "1:451729452961:web:0fe79d8bc55ff6d20125e0",
  measurementId: "G-X7MS0B9E74"
};

firebase.initializeApp(firebaseConfig);
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
const database = getDatabase(app);

var contactFormDB = firebase.database().ref('EldenForum');
function displayThreads(){
       var container = document.querySelector('ol');
       container.innerHTML = '';
        contactFormDB.on("value",(snapshot)=>{
            const data = snapshot.val();
            for (let i in data){
            const thread = data[i];
            var html = `
            <li class = "row">
            <a id = "url" href = "thread.html?${thread.threadid}">
            <h4 class = "title">
                ${thread.title}
            </h4>
            <div class = "desc" style = "font-size:20px;">
                ${thread.description}
            </div>
            <div class = "bottom">
                <p class = "timestamp" style = "font-size: 15px;">
                ${thread.username} ${thread.dateposted}
                </p>
            </div>
            </a>
            </li> `
            container.insertAdjacentHTML('beforeend', html);
            }
            
        });
      }
      displayThreads();
        /*
        var header = document.querySelector('ol');
        contactFormDB.on("value",(snapshot)=>{
            const data = snapshot.val();
            var html = '';
            for (let i in data){
              const thread = data[i];
              html += `
                <li class = "row">
                <h4 class = "title">
                    ${thread.title}
                </h4>
                <div class = "bottom">
                    <p class = "timestamp">
                    ${storedComments.length} ${thread.timestamp}
                    </p>
                </div>
                </li> `
              }
              header.insertAdjacentHTML('beforeend', html);
        });*/

          function getCurrentDate() {
            const today = new Date();
            const year = today.getFullYear();
            let month = today.getMonth() + 1;
            let day = today.getDate();
            month = month < 10 ? '0' + month : month;
            day = day < 10 ? '0' + day : day;

            return `${year}-${month}-${day}`;
          }


//search threads
function filterThreads(searchTerm) {
  var container = document.querySelector('ol');
  container.innerHTML = ''; // Clear the container
  contactFormDB.on("value",(snapshot)=>{
    const data = snapshot.val();
    for (let i in data){
    const thread = data[i];
    // Check if the thread title contains the search term
    console.log(searchTerm)
    if(searchTerm != ''){
      if (thread.title.toLowerCase().includes(searchTerm.toLowerCase())) {
        var html = `
            <li class="row">
                <a id="url" href="thread.html?${thread.threadid}" onClick="saveId(${thread.threadid})">
                    <h4 class="title">${thread.title}</h4>
                    <div class="desc" style="font-size:20px;">${thread.description}</div>
                    <div class="bottom">
                        <p class="timestamp">${thread.username} ${thread.dateposted}</p>
                    </div>
                </a>
            </li>`;
        container.insertAdjacentHTML('beforeend', html);
      }
    }
    else{
      displayThreads();
    }
  }}
)};

searchInput.addEventListener('input', function () {
  filterThreads(this.value);
});


//side navbar filters
document.getElementById('Viewall').addEventListener('click', function(event) {
  event.preventDefault();
  var container = document.querySelector('ol');
  container.innerHTML = '';
        contactFormDB.on("value",(snapshot)=>{
            const data = snapshot.val();
            for (let i in data){
            const thread = data[i];
            var html = `
            <li class = "row">
            <a href = "/thread.html?${thread.id}">
            <h4 class = "title">
                ${thread.title}
            </h4>
            <div class = "desc" style = "font-size:20px;">
                ${thread.description}
            </div>
            <div class = "bottom" style = "font-size:20px;">
                <p class = "timestamp">
                ${thread.username} ${thread.dateposted}
                </p>
            </div>
            </a>
            </li> `
            container.insertAdjacentHTML('beforeend', html);
          }
      });
});

document.getElementById('bankingthreads').addEventListener('click', function(event) {
  event.preventDefault(); 
  var container = document.querySelector('ol');
  container.innerHTML = '';
      contactFormDB.on("value",(snapshot)=>{
          const data = snapshot.val();
          for (let i in data){
          const thread = data[i];
          var html = `
          <li class = "row">
          <a href = "/thread.html?${thread.id}">
          <h4 class = "title">
              ${thread.title}
          </h4>
          <div class = "desc" style = "font-size:20px;">
              ${thread.description}
          </div>
          <div class = "bottom" style = "font-size:20px;">
              <p class = "timestamp">
              ${thread.username} ${thread.dateposted}
              </p>
          </div>
          </a>
          </li> `
          if(thread.category === 'Banking'){
          container.insertAdjacentHTML('beforeend', html);
          }
        }
      });
});

  document.getElementById('telecommthreads').addEventListener('click', function(event) {
    event.preventDefault(); 
    var container = document.querySelector('ol');
    container.innerHTML = '';
        contactFormDB.on("value",(snapshot)=>{
            const data = snapshot.val();
            for (let i in data){
            const thread = data[i];
            var html = `
            <li class = "row">
            <a href = "/thread.html?${thread.id}">
            <h4 class = "title">
                ${thread.title}
            </h4>
            <div class = "desc" style = "font-size:20px;">
                ${thread.description}
            </div>
            <div class = "bottom" style = "font-size:20px;">
                <p class = "timestamp">
                ${thread.username} ${thread.dateposted}
                </p>
            </div>
            </a>
            </li> `
            if(thread.category === 'Telecommunication'){
            container.insertAdjacentHTML('beforeend', html);
            }
          }
        });
});

document.getElementById('healththreads').addEventListener('click', function(event) {
  event.preventDefault(); 
  var container = document.querySelector('ol');
  container.innerHTML = '';
      contactFormDB.on("value",(snapshot)=>{
          const data = snapshot.val();
          for (let i in data){
          const thread = data[i];
          var html = `
          <li class = "row">
          <a href = "/thread.html?${thread.id}">
          <h4 class = "title">
              ${thread.title}
          </h4>
          <div class = "desc" style = "font-size:20px;">
              ${thread.description}
          </div>
          <div class = "bottom" style = "font-size:20px;">
              <p class = "timestamp">
              ${thread.username} ${thread.dateposted}
              </p>
          </div>
          </a>
          </li> `
          if(thread.category === 'Health'){
          container.insertAdjacentHTML('beforeend', html);
          }
        }
      });
});

document.getElementById('transportthreads').addEventListener('click', function(event) {
  event.preventDefault(); 
  var container = document.querySelector('ol');
  container.innerHTML = '';
      contactFormDB.on("value",(snapshot)=>{
          const data = snapshot.val();
          for (let i in data){
          const thread = data[i];
          var html = `
          <li class = "row">
          <a href = "/thread.html?${thread.id}">
          <h4 class = "title">
              ${thread.title}
          </h4>
          <div class = "desc" style = "font-size:20px;">
              ${thread.description}
          </div>
          <div class = "bottom" style = "font-size:20px;">
              <p class = "timestamp">
              ${thread.username} ${thread.dateposted}
              </p>
          </div>
          </a>
          </li> `
          if(thread.category === 'Transport'){
          container.insertAdjacentHTML('beforeend', html);
          }
        }
      });
});

document.getElementById('foodthreads').addEventListener('click', function(event) {
  event.preventDefault(); 
  var container = document.querySelector('ol');
  container.innerHTML = '';
      contactFormDB.on("value",(snapshot)=>{
          const data = snapshot.val();
          for (let i in data){
          const thread = data[i];
          var html = `
          <li class = "row">
          <a href = "/thread.html?${thread.id}">
          <h4 class = "title">
              ${thread.title}
          </h4>
          <div class = "desc" style = "font-size:20px;">
              ${thread.description}
          </div>
          <div class = "bottom" style = "font-size:20px;">
              <p class = "timestamp">
              ${thread.username} ${thread.dateposted}
              </p>
          </div>
          </a>
          </li> `
          if(thread.category === 'Food Delivery'){
          container.insertAdjacentHTML('beforeend', html);
          }
        }
      });
});