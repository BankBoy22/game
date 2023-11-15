// soundManager.js

class SoundManager {
    constructor() {
        this.audioContext = null;
        this.audioBuffer = null;
        this.isPlaying = false;
        this.loop = false;
        this.source = null;
    }

    initAudioContext() {
        // 오디오 컨텍스트 초기화
        if (!this.audioContext) {
            this.audioContext = new (window.AudioContext || window.webkitAudioContext)();
        }
    }

    loadSound(filePath, callback) {
        const request = new XMLHttpRequest();
        request.open('GET', filePath, true);
        request.responseType = 'arraybuffer';

        request.onload = () => {
            this.audioContext.decodeAudioData(
                request.response,
                (buffer) => {
                    this.audioBuffer = buffer;
                    callback(null);
                },
                (error) => {
                    callback(`Error decoding audio data: ${error}`);
                }
            );
        };

        request.onerror = () => {
            callback(`Error loading audio: ${filePath}`);
        };

        request.send();
    }

    play() {
        if (this.audioBuffer) {
            this.source = this.audioContext.createBufferSource();
            this.source.buffer = this.audioBuffer;
            this.source.loop = this.loop;

            this.source.connect(this.audioContext.destination);
            this.source.start(0);
            this.isPlaying = true;
        }
    }
    
    // 나머지 메서드 생략
}

const soundManager = new SoundManager();
export default soundManager;
