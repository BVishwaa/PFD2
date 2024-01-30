document.addEventListener('DOMContentLoaded', function () {
    // Event listener for all category buttons
    ['all', 'Banking', 'Health', 'Telecomunication', 'Food Delivery', 'Transport'].forEach(function (id) {
        document.getElementById(id).addEventListener('click', function (event) {
            event.preventDefault();
            filterThreads(this.id);
        });
    });
});

function filterThreads(categoryId) {
    var threads = document.querySelectorAll('.thread-item');
    var category = categoryId.replace('threads', ''); // Extract category from id
    if (category === 'all') {
        category = ''; // If 'Viewall', show all threads
    }

    threads.forEach(function (thread) {
        var threadCategories = thread.getAttribute('data-category').split(',');
        if (category === '' || threadCategories.includes(category)) {
            thread.style.display = ''; // Show thread
        } else {
            thread.style.display = 'none'; // Hide thread
        }
    });
}
