window.playByteArray = (bytes) => {
    // Crea un Blob dai byte ricevuti
    const blob = new Blob([bytes], { type: 'audio/mpeg' });
    const url = URL.createObjectURL(blob);
    const audio = new Audio(url);
    audio.play();
    
    // Pulisce la memoria dopo la riproduzione
    audio.onended = () => URL.revokeObjectURL(url);
};