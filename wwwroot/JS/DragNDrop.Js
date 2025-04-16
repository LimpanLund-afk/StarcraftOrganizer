window.initializeDragAndDrop = (dragElementsIds, dropElementsIds) => {
    // Gör element som ska dras "draggable"
    dragElementsIds.forEach(id => {
        const element = document.getElementById(id);
        if (element) {
            element.setAttribute("draggable", "true");

            element.addEventListener("dragstart", function (e) {
                e.dataTransfer.setData("text/plain", e.target.id); // Skicka med id eller annan data
            });
        }
    });

    // Gör element som ska ta emot droppade objekt
    dropElementsIds.forEach(id => {
        const element = document.getElementById(id);
        if (element) {
            element.addEventListener("dragover", function (e) {
                e.preventDefault(); // Förhindra standardbeteende för att tillåta drop
            });

            element.addEventListener("drop", function (e) {
                e.preventDefault();
                const draggedElementId = e.dataTransfer.getData("text/plain");
                const draggedElement = document.getElementById(draggedElementId);
                if (draggedElement) {
                    element.appendChild(draggedElement); // Flytta objektet till nytt element
                }
            });
        }
    });
};