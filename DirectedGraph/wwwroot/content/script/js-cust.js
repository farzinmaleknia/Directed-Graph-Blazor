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