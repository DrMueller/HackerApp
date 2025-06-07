export function zoom(zoomIn) {
    const zoomStep = 0.2;
    const body = document.body;
    const current = parseFloat(body.style.zoom || "1");
    const newZoom = zoomIn ? current + zoomStep : Math.max(0.2, current - zoomStep);

    body.style.zoom = newZoom.toString();
}