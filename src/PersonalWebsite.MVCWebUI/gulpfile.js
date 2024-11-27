let gulp = require('gulp');
let cleanCSS = require('gulp-clean-css');
let rename = require('gulp-rename');

// Copy npm packages to wwwroot/lib directory.
gulp.task('copy-libs', async function () {
    // bootstrap.
    gulp.src('node_modules/bootstrap/dist/**/*')
        .pipe(gulp.dest('wwwroot/lib/bootstrap'));
    // jquery.
    gulp.src('node_modules/jquery/dist/**/*')
        .pipe(gulp.dest('wwwroot/lib/jquery'));
    // jquery-validation.
    gulp.src('node_modules/jquery-validation/dist/**/*')
        .pipe(gulp.dest('wwwroot/lib/jquery-validation'));
    // jquery-validation-unobtrusive.
    gulp.src('node_modules/jquery-validation-unobtrusive/dist/**/*')
        .pipe(gulp.dest('wwwroot/lib/jquery-validation-unobtrusive'));
});

// Minimize the css files.
gulp.task('clean-css', async function () {
    gulp.src('wwwroot/css/**/*css')
        .pipe(cleanCSS())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css'));
});

// Set default task for gulp.
gulp.task('default', gulp.series('copy-libs', 'clean-css'));
