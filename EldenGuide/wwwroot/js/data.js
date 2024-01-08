// Assume comments is an array of comment objects
var comments = [ 
    {id: 1, author: "guest", datecommented: "2021-10-11", content: "This is comment 1"},
    {id: 1, author: "guest", datecommented: "2022-08-12", content: "This is comment 2" },
    {id: 1, author: "guest", datecommented: "2023-07-15", content: "This is comment 3" },
    {id: 2, author: "guest", datecommented: "2022-11-21", content: "This is comment 1" },
    {id: 2, author: "guest", datecommented: "2021-12-26", content: "This is comment 2" },
    {id: 3, author: "guest", datecommented: "2021-06-30", content: "This is comment 1" },
    {id: 4, author: "guest", datecommented: "2022-05-09", content: "This is comment 1" },
    {id: 5, author: "guest", datecommented: "2023-01-03", content: "This is comment 1" },
    {id: 6, author: "guest", datecommented: "2023-10-11", content: "This is comment 1" }
    // ... more comments
  ];
  
  // Convert the comments array to a JSON string
  var commentsJson = JSON.stringify(comments);
  
  // Save the JSON string to local storage under a specific key
  localStorage.setItem('forumComments', commentsJson);

  