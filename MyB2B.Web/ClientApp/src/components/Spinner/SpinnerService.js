export class SpinnerService {
    constructor() {
        this.spinnerCache = new Set();
    }

    _register(spinner) {
        console.log("spinner " + spinner.name + " registered");
        this.spinnerCache.add(spinner);
    }

    _unregister(spinnerToRemove) {
        console.log("spinner " + spinnerToRemove.name + " unregistered");
        this.spinnerCache.forEach(spinner => {
            if(spinner === spinnerToRemove) {
                this.spinnerCache.delete(spinner);
            }
        });
    }

    _unregisterGroup(spinnerGroup) {
        this.spinnerCache.forEach(spinner => {
            if(spinner.group === spinnerGroup) {
                this.spinnerCache.delete(spinner);
            }
        });
    }

    _unregisterAll() {
        this.spinnerCache.clear();
    }

    showGlobalSpinner() {
        return this.show('global-spinner');
    }

    hideGlobalSpinner() {
        return this.hide('global-spinner');
    }

    show(spinnerName) {
        this.spinnerCache.forEach(spinner => {
            if(spinner.name === spinnerName) {
                spinner.show = true;
            }
        });
    }

    hide(spinnerName) {
        this.spinnerCache.forEach(spinner => {
            if(spinner.name === spinnerName) {
                spinner.show = false;
            }
        });
    }

    showGroup(spinnerGroup) {
        this.spinnerCache.forEach(spinner => {
            if(spinner.group === spinnerGroup) {
                spinner.show = true;
            }
        });
    }

    hideGroup(spinnerGroup) {
        this.spinnerCache.forEach(spinner => {
            if(spinner.group === spinnerGroup) {
                spinner.show = false;
            }
        });
    }

    showAll() {
        this.spinnerCache.forEach(spinner => spinner.show = true);
    }

    hideAll() {
        this.spinnerCache.forEach(spinner => spinner.show = false);
    }

    isShowing(spinnerName) {
        let showing;
        this.spinnerCache.forEach(spinner => {
            if(spinner.name === spinnerName) {
                showing = spinner.shadow;
            }
        });

        return showing;
    }
}

const spinnerService = new SpinnerService();
export { spinnerService };