function getElementDimension(id) {
    const dimention = document.getElementById(`${id}`);
    console.log(id);
    if (dimention) {
        var offsets = dimention.getBoundingClientRect();
        console.log(2);
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
    var zoomable = document.getElementById(`${id}`); //.getElementsByTagName('p')
    zX = 1;

    console.log( "befor" , zoomable.getBoundingClientRect().width)
    window.addEventListener('wheel', function (e) {
        var dir;
        dir = (e.deltaY > 0) ? -0.1 : 0.1;
        zX += dir;

        zoomable.style.transform = 'scale(' + zX + ')';

        e.preventDefault();
        return;
    }, { passive: false });


    zoomable.addEventListener('mousedown', function (e) {
        isDown = true;
        offset = [
            zoomable.offsetLeft - e.clientX,
            zoomable.offsetTop - e.clientY
        ];
        zoomable.style.cursor = "grabbing";
    }, true);

    document.addEventListener('mouseup', function () {
        isDown = false;
        zoomable.style.cursor = "pointer";
    }, true);

    document.addEventListener('mousemove', function (event) {
        event.preventDefault();
        if (isDown) {
            mousePosition = {

                x: event.clientX,
                y: event.clientY

            };
            zoomable.style.left = (mousePosition.x + offset[0]) + 'px';
            zoomable.style.top = (mousePosition.y + offset[1]) + 'px';
        }
    }, true);

}