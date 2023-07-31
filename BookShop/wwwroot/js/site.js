// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Product/GetAll' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    console.log(data);
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/Product/Edit?id=${data}" class="btn bt-sm btn-warning mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>  
                     <a data-id="${data}" class="btn btn-sm btn-danger delete-Item"><i class="bi bi-trash3"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}
