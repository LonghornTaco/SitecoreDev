/// <binding AfterBuild='Run-Build' />
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var gulpConfig = require("./gulp-config.js")();
var serviceController = require("windows-service-controller");
var deleteFiles = require("del");
var runSequence = require("run-sequence");
module.exports.config = gulpConfig;

gulp.task("Run-Build", function () {
   gulp.pipe(stopIIS())
   stopIIS();
   //deleteFiles();
   //publishSite();
   startIIS();
});

var publishSite = function () {
   return gulp.src("./{Feature,Foundation,Project}/**/**/*.csproj")
     .pipe(foreach(function (stream, file) {
        return stream
          .pipe(debug({ title: "Publishing " }))
          .pipe(msbuild({
             targets: ["Build"],
             gulpConfiguration: gulpConfig.buildConfiguration,
             verbosity: "diagnostic",
             //errorOnFail: true,
             //stdout: true,
             maxcpucount: 0,
             toolsVersion: 14.0,
             properties: {
                publishUrl: gulpConfig.webRoot,
                DeployDefaultTarget: "WebPublish",
                WebPublishMethod: "FileSystem",
                DeployOnBuild: "true",
                DeleteExistingFiles: "false",
                _FindDependencies: "false"
             }
          }));
     }));
};

var stopIIS = function () {
   return serviceController.stop('W3SVC')
      .catch(function (error) {
         console.log('Error stopping IIS: ' + error.message);
      })
      .done(function () {
         console.log('IIS Stopped');
      });
};
var startIIS = function () {
   return serviceController.start('W3SVC')
      .catch(function (error) {
         console.log('Error starting IIS: ' + error.message);
      })
      .done(function () {
         console.log('IIS Started');
      });
};
var deleteFiles = function () {
   var path = gulpConfig.webRoot + '\\bin\\SitecoreDev.*';
   console.log('Deleting files ' + path);
   return deleteFiles(path, {force:true});
};

//if build is failing, it's probably a permissions issue on the SitecoreDev.*.dlls.  Stop IIS, delete them, restart IIS