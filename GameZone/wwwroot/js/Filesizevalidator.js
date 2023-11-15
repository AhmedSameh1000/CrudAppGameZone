$.validator.addMethod('filesize', function (value, ele, param) {
    return this.optional(ele) || ele.files[0].size <= param
});