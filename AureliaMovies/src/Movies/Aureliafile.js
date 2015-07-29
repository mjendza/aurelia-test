var aurelia = require("aurelia-cli");

aurelia.command("bundle", {
    js: {
        "wwwroot/appbundle": {
            modules: [
                "app", "main", "about/**", "movies/**", "resources/**",
                "aurelia-bootstrapper",
                "aurelia-framework",
                "aurelia-router",
                "aurelia-http-client",
                "aurelia-validation"
            ],
            options: {
                inject: true, minify:false
            }
        }
    },
    template: {
        "wwwroot/appbundle": {
            pattern: "./wwwroot/**/*.html",
            options: {}
        }
    }
});