import gulp from 'gulp';
import cleanCSS from 'gulp-clean-css';
import rename from 'gulp-rename';
import filter from 'gulp-filter';

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
    let cssFilter = filter(['**/*.css', '!**/*.min.css'], { restore: true });
    gulp.src('wwwroot/css/**/*css')
        .pipe(cssFilter)
        .pipe(cleanCSS({ rebase: false }))
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css'))
        .pipe(cssFilter.restore);;
});

// Set default task for gulp.
gulp.task('default', gulp.series('copy-libs', 'clean-css'));
