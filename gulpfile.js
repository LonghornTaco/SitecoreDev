
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var gulpConfig = require("./gulp-config.js")();
var serviceController = require("windows-service-controller");
var deleteFiles = require("del");
var runSequence = require("run-sequence");
var newer = require("gulp-newer");
var cssmin = require("gulp-cssmin");
var rename = require("gulp-rename");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");
module.exports.config = gulpConfig;

//gulp.task("Run-Build", ["stop-iis", "delete-assemblies", "publish-site"], function () {
   
//});

gulp.task("stop-iis", function () {
   return serviceController.stop('W3SVC')
      .catch(function (error) {
         console.log('Error stopping IIS: ' + error.message);
      })
      .done(function () {
         console.log('IIS Stopped');
      });
});

gulp.task("start-iis", function () {
   return serviceController.start('W3SVC')
      .catch(function (error) {
         console.log('Error starting IIS: ' + error.message);
      })
      .done(function () {
         console.log('IIS Started');
      });
});

gulp.task("delete-assemblies", function () {
  gulp.start("delete-project-assemblies");
  gulp.start("delete-foundation-assemblies");
  gulp.start("delete-feature-assemblies");
});

gulp.task("delete-feature-assemblies", function () {
  deleteFiles([
     gulpConfig.webRoot + '\\bin\\SitecoreDev.Feature.*',
  ],
     { force: true });
});
gulp.task("delete-foundation-assemblies", function () {
  deleteFiles([
     gulpConfig.webRoot + '\\bin\\SitecoreDev.Foundation.*',
  ],
     { force: true });
});
gulp.task("delete-project-assemblies", function () {
  deleteFiles([
     gulpConfig.webRoot + '\\bin\\SitecoreDev.Website.*',
  ],
     { force: true });
});

gulp.task("publish-foundation", function() {
   return publishProject("./Foundation");
});
gulp.task("publish-project", function() {
   return publishProject("./Project");
});
gulp.task("publish-feature", function() {
   return publishProject("./Feature");
});

gulp.task("publish-views", function () {
   var root = "./";
   var roots = [root + "**/Views", "!" + root + "/**/obj/**/Views"];
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
gulp.task("view-watcher", function () {
   var root = "./";
   var roots = [root + "**/Views", "!" + root + "/**/obj/**/Views"];
   var files = "/**/*.cshtml";
   var destination = gulpConfig.webRoot + "\\Views";
   gulp.src(roots, { base: root }).pipe(
     foreach(function (stream, rootFolder) {
        gulp.watch(rootFolder.path + files, function (event) {
           if (event.type === "changed") {
              console.log("publish this file " + event.path);
              gulp.src(event.path, { base: rootFolder.path }).pipe(gulp.dest(destination));
           }
           console.log("published " + event.path);
        });
        return stream;
     })
   );
});
gulp.task("publish-pdbs", function () {
  publishPDBs();
});
//gulp.task("publish-site", function () {
//   return gulp.src("./{Feature,Foundation,Project}/**/**/*.csproj")
//     .pipe(foreach(function (stream, file) {
//        return stream
//          .pipe(debug({ title: "Publishing " }))
//          .pipe(msbuild({
//             targets: ["Build"],
//             gulpConfiguration: gulpConfig.buildConfiguration,
//             verbosity: "minimal",
//             errorOnFail: true,
//             stdout: true,
//             maxcpucount: 0,
//             toolsVersion: 14.0,
//             properties: {
//                DeployOnBuild: "true",
//                DeployDefaultTarget: "WebPublish",
//                WebPublishMethod: "FileSystem",
//                DeleteExistingFiles: "false",
//                publishUrl: gulpConfig.webRoot,
//                _FindDependencies: "false"
//             }
//          }));
//     }));
//});
//gulp.task("minify-css", function () {
//  minifyCss();
//});
//gulp.task("minify-js", function () {
//  minifyJs
//});
//gulp.task("css-watcher", function () {
//  var root = "./";
//  var roots = [root + "**/Content", "!" + root + "/**/obj/**/Content"];
//  var files = "/**/*.css";
//  var destination = gulpConfig.webRoot + "\\Content";
//  gulp.src(roots, { base: root }).pipe(
//    foreach(function (stream, rootFolder) {
//      gulp.watch(rootFolder.path + files, function (event) {
//        if (event.type === "changed") {
//          console.log("publish this file " + event.path);
//          minifyCss(destination);
//        }
//        console.log("published " + event.path);
//      });
//      return stream;
//    })
//  );
//});
//gulp.task("js-watcher", function () {
//  var root = "./";
//  var roots = [root + "**/Scripts", "!" + root + "/**/obj/**/Scripts"];
//  var files = "/**/*.js";
//  var destination = gulpConfig.webRoot + "\\Scripts";
//  gulp.src(roots, { base: root }).pipe(
//    foreach(function (stream, rootFolder) {
//      gulp.watch(rootFolder.path + files, function (event) {
//        if (event.type === "changed") {
//          console.log("publish this file " + event.path);
//          minifyJs(destination);
//        }
//        console.log("published " + event.path);
//      });
//      return stream;
//    })
//  );
//});
//var minifyCss = function (destination) {
//  gulp.src("./{Feature,Foundation,Project}/**/**/Content/*.css")
//    .pipe(concat('sitecoredev.min.css'))
//    .pipe(cssmin())
//    .pipe(gulp.dest(destination));
//};
//var minifyJs = function (destination) {
//  gulp.src("./{Feature,Foundation,Project}/**/**/Scripts/*.js")
//    .pipe(concat('sitecoredev.min.js'))
//    .pipe(uglify())
//    .pipe(gulp.dest(destination));
//};
var publishProject = function (source) {
   console.log("Publishing " + source + " to " + gulpConfig.webRoot);
   return gulp.src(source + "/**/**/*.csproj")
     .pipe(foreach(function (stream, file) {
        return stream
          .pipe(debug({ title: "Publishing " }))
          .pipe(msbuild({
             targets: ["Build"],
             gulpConfiguration: "Debug",
             verbosity: "minimal",
             //errorOnFail: true,
             //stdout: true,
             maxcpucount: 0,
             toolsVersion: 14.0,
             properties: {
                DeployOnBuild: "true",
                DeployDefaultTarget: "WebPublish",
                WebPublishMethod: "FileSystem",
                DeleteExistingFiles: "false",
                publishUrl: gulpConfig.webRoot,
                _FindDependencies: "false"
             }
          }));
     }));
};
var publishPDBs = function () {
  var roots = ["./{Feature,Foundation,Project}/**/**/bin"];
  var files = "/SitecoreDev.*.pdb";
  var destination = gulpConfig.webRoot + "/bin";
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
}