// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.








var toolbarOptions = [
  ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
  ['blockquote', 'code-block', 'image'],
  
  [{ 'header': 1 }, { 'header': 2 }],
  // [{ 'list': ['ordered', 'bullet']}],             // custom button values
  [{ 'list': 'ordered'}, { 'list': 'bullet' }],
  [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
  [{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
  [{ 'direction': 'rtl' }],                         // text direction
  
  [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
  [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
  
  [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
  [{ 'font': [] }],
  [{ 'align': [] }],
  ['link', 'image']
  
  ['clean']                                         // remove formatting button
];

var quill = new Quill('#editor', {
  modules: {
    toolbar: toolbarOptions
  },
  placeholder: 'Sizning hikoyangiz...',
  theme: 'bubble'
});

// var quill = new Quill('#muallif', {
//   modules: {
//     toolbar: toolbarOptions
//   },
//   placeholder: 'Muallif',
//   theme: 'bubble'
// });

// var quill = new Quill('#title-editor', {
//   modules: {
//     toolbar: toolbarOptions
//   },
//   placeholder: 'Sarlavha...',
//   theme: 'bubble'
// });



document.addEventListener('DOMContentLoaded', function(event) {


  var content = document.getElementById('content');
  
  quill.container.firstChild.innerHTML = content.value

  var form = document.getElementById('form');
  form.onsubmit = function () {
      // Populate hidden form on submit
      content.value = quill.root.innerHTML;
  };
})




// const toggleSwitch = document.querySelector('.theme-switch input[type="checkbox"]');

// function switchTheme(e) {
//     if (e.target.checked) {
//         document.documentElement.setAttribute('data-theme', 'dark');
//     }
//     else {
//         document.documentElement.setAttribute('data-theme', 'light');
//     }    
// }

// toggleSwitch.addEventListener('change', switchTheme, false);



// function switchTheme(e) {
//   if (e.target.checked) {
//       document.documentElement.setAttribute('data-theme', 'dark');
//       localStorage.setItem('theme', 'dark'); //add this
//   }
//   else {
//       document.documentElement.setAttribute('data-theme', 'light');
//       localStorage.setItem('theme', 'light'); //add this
//   }    
// }



// const currentTheme = localStorage.getItem('theme') ? localStorage.getItem('theme') : null;

// if (currentTheme) {
//     document.documentElement.setAttribute('data-theme', currentTheme);

//     if (currentTheme === 'dark') {
//         toggleSwitch.checked = true;
//     }
// }