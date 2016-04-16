var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var newer = require("gulp-newer");
var runSequence = require("run-sequence");
var path = require("path");
var gulpConfig = require("./gulp-config.js")();
module.exports.config = gulpConfig;

/*****************************
  Publish
*****************************/
var publishProjects = function (location, dest) {
   dest = dest || gulpConfig.webRoot;
   console.log("publish to " + dest + " folder");
   return gulp.src([location + "/**/**/*.csproj"])
     .pipe(foreach(function (stream, file) {
        return stream
          .pipe(debug({ title: "Building project:" }))
          .pipe(msbuild({
             targets: ["Build"],
             gulpConfiguration: gulpConfig.buildConfiguration,
             logCommand: false,
             verbosity: "minimal",
             maxcpucount: 0,
             toolsVersion: 14.0,
             properties: {
                DeployOnBuild: "true",
                DeployDefaultTarget: "WebPublish",
                WebPublishMethod: "FileSystem",
                DeleteExistingFiles: "false",
                publishUrl: dest,
                _FindDependencies: "false"
             }
          }));
     }));
};

gulp.task("Publish-All-Projects", function (callback) {
   runSequence(
     "Publish-Foundation-Projects",
     "Publish-Feature-Projects",
     "Publish-Project-Projects", callback);
});

gulp.task("Publish-Foundation-Projects", function () {
   return publishProjects("./Foundation");
});

gulp.task("Publish-Feature-Projects", function () {
   return publishProjects("./Feature");
});

gulp.task("Publish-Project-Projects", function () {
   return publishProjects("./Project");
});

gulp.task("Publish-All-Views", function () {
   var root = "./";
   var roots = [root + "/**/Views", "!" + root + "/**/obj/**/Views"];
   var files = "/**/*.cshtml";
   var destination = gulpConfig.webRoot + "\\Views";
   return gulp.src(roots, { base: root }).pipe(
     foreach(function (stream, file) {
        console.log("Publishing from " + file.path);
        gulp.src(file.path + files, { base: file.path })
          .pipe(newer(destination))
          .pipe(debug({ title: "Copying " }))
          .pipe(gulp.dest(destination));
        return stream;
     })
   );
});

gulp.task("Publish-All-Configs", function () {
   var root = "./";
   var roots = [root + "/**/App_Config", "!" + root + "/**/obj/**/App_Config"];
   var files = "/**/*.config";
   var destination = gulpConfig.webRoot + "\\App_Config";
   return gulp.src(roots, { base: root }).pipe(
     foreach(function (stream, file) {
        console.log("Publishing from " + file.path);
        gulp.src(file.path + files, { base: file.path })
          .pipe(newer(destination))
          .pipe(debug({ title: "Copying " }))
          .pipe(gulp.dest(destination));
        return stream;
     })
   );
});
