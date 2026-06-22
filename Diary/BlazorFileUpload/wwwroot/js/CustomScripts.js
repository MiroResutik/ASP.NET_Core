function downloadFile(fileDataUrl) {
    fetch(fileDataUrl).then(response => response.blob()).then(blob => {

        // Create link with element a <a>
        var link = window.document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.download = "download_" + new Date().toISOString().slice(0, 10);

        // dynamicali add links based on response
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);


    })
}