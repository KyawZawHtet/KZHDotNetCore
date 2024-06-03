const tblBlog = "blogs";
let blogId = null;

getBlogTable();

// readBlog();
// createBlog();
// updateBlog("05394e02-9fa8-4ee6-8f68-dca8ec6033e9","Kyaw","Zaw","Htet");
// deleteBlog("05394e02-9fa8-4ee6-8f68-dca8ec6033e9");

function readBlog() {
    let lst = getBlogs();
    console.log(lst);
}

function editBlog(id){
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length)
    if (items.length === 0) {
        console.log("No data found");
        errorMessage("No data found");
        return;
    }

    // return items[0];

    let item = items[0];

    blogId = item.id;
    $('#title').val(item.title);
    $('#author').val(item.author);
    $('#content').val(item.content);
    $('#title').focus();
}

function createBlog(title, author, content) {
    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content,
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);
    // localStorage.setItem("blogs", requestModel);

    successMessage("Saving Successfully");
    clearControls();
}

function updateBlog(id, title, author, content) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length)
    if (items.length === 0) {
        console.log("No data found");
        errorMessage("No data found");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Updating successfully");
}

function deleteBlog(id) {
    let result = confirm("Are you sure you want to delete.")
    if(!result) return;
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items.length)
    if (items.length === 0) {
        console.log("No data found");
        return;
    }

    lst = lst.filter(x => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Deleting successfully");
    getBlogTable();
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    // 1 == '1' => true
    // 1 === '1' => false

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}


$('#btnSave').click(function () {
    const title = $('#title').val();
    const author = $('#author').val();
    const content = $('#content').val();

    if(blogId === null){
        createBlog(title, author, content);
    }else {
        updateBlog(blogId, title, author, content);
        blogId = null;
    }

    getBlogTable();
});

function successMessage(message) {
    alert(message);
}

function errorMessage(message) {
    alert(message);
}

function clearControls() {
    $('#title').val('');
    $('#author').val('');
    $('#content').val('');
    $('#title').focus();
}

function getBlogTable() {
    const lst = getBlogs();
    let htmlRows = '';
    let count = 0;
    lst.forEach(item => {
        const htmlRow = `
        <tr>
          <td>
            <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
            <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
          </td>
          <td>${++count}</td>
          <td>${item.title}</td>
          <td>${item.author}</td>
          <td>${item.content}</td>
        </tr>
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
}
