
function getElementDimension(id) {
    const dimention = document.getElementById(`${id}`);
    if (dimention) {
        var offsets = dimention.getBoundingClientRect();
        return {
            height: `${dimention.offsetHeight}`,
            width: `${dimention.offsetWidth}`,
            top: `${offsets.top}`,
            left: `${offsets.left}`,
            right: `${offsets.right}`,
        }

    } else {
        return (null);
    }

}

function makeDivZoomable(id) {

    var isDown = false;
    var div = document.getElementById(`${id}`); //.getElementsByTagName('p')
    zX = 1;

    window.addEventListener('wheel', function (e) {
        var dir;
        dir = (e.deltaY > 0) ? -0.1 : 0.1;


        // returns matrix(1,0,0,1,0,0)
        var matrix = window.getComputedStyle(div).transform;
        var matrixArray = matrix.replace("matrix(", "").split(",");
        var preScale = parseFloat(matrixArray[0]);

        var newScale = e.deltaY > 0 ? preScale - 0.1 : preScale + 0.1;

        var rec = div.getBoundingClientRect();

        mousePosition = {

            x: e.clientX - div.offsetLeft,
            y: e.clientY - div.offsetTop

        };

        if (!isNaN(preScale)) {
            var newTx = (mousePosition.x ) / 1;
            var newTy = (mousePosition.y ) / 1;


            div.style.transformOrigin = `${newTx}px ${newTy}px`;

        }

        console.log(`newScale ${newScale} - preScale ${preScale} - `)
        if (newScale > 0.09 || preScale != 0.1) {

            zX += dir;
            div.style.transform = `scale(${zX})`;
            //div.style.transform = `translate(${newTx}, ${newTy})`;

            //div.style.translate.x += (-newTx * dir * 2) + (div.offsetWidth * dir);
            //div.style.translate.y += (-newTy * dir * 2) + (div.offsetHeight * dir);
        }



        e.preventDefault();
        return;
    }, { passive: false });


    div.addEventListener('mousedown', function (e) {
        isDown = true;
        offset = [
            div.offsetLeft - e.clientX,
            div.offsetTop - e.clientY
        ];
        div.style.cursor = "grabbing";
    }, true);

    document.addEventListener('mouseup', function () {
        isDown = false;
        div.style.cursor = "pointer";
    }, true);

    document.addEventListener('mousemove', function (event) {
        event.preventDefault();
        if (isDown) {
            mousePosition = {

                x: event.clientX,
                y: event.clientY

            };
            div.style.left = (mousePosition.x + offset[0]) + 'px';
            div.style.top = (mousePosition.y + offset[1]) + 'px';
        }
    }, true);

}

function refreshScale(id, top, left) {

    var div = document.getElementById(`${id}`);
    div.style.transform = 'scale(1)';
    div.style.transformOrigin = `${div.offsetLeft + (div.width / 2)}px ${div.offsetTop + (div.height / 2)}px`;
    div.style.top = top + 'px';
    div.style.left = left + 'px';
    zX = 1;
}