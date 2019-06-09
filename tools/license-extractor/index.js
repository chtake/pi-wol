var nlf = require('nlf');
const fs = require('fs');

var projectRoot = '../../src/PiWol.WebApp/ClientApp';

var licenseWeb = {
    'MIT':'https://opensource.org/licenses/MIT',
    'BSD-3-Clause':'https://opensource.org/licenses/BSD-3-Clause',
    'Apache-2.0':'https://opensource.org/licenses/Apache-2.0'
}

var fileContent = '#Third party libraries used by pi-wol\n\n'+
'Software | Version | License\n'+
'--- | --- | ---\n'+
'[IPAddressRange](https://github.com/jsakamoto/ipaddressrange) | 3.2.0 | [Mozilla Public License, version 2.0](https://github.com/jsakamoto/ipaddressrange/blob/master/LICENSE)\n'+
'[FluentScheduler](https://github.com/fluentscheduler/FluentScheduler) | 5.3.0 | [BSD-3-Clause](https://opensource.org/licenses/BSD-3-Clause)\n'+
'[Node License Finder](https://github.com/iandotkelly/nlf) | 2.1.1 | [MIT](https://opensource.org/licenses/MIT)';

nlf.find({ 
    directory: projectRoot,
    production: true,
    depth: 1

}, function (err, data) {
    data.forEach(element => {
        if ( element.name !== 'PiWol.WebApp') {
            fileContent += '['+element.name+']('+element.repository+') | '+element.version+' | ['+element.licenseSources.package.sources[0].license+']('+licenseWeb[element.licenseSources.package.sources[0].license]+')\n';
        }
    });
    fs.writeFile("../../3rd-Party-LICENSES.md", fileContent, function(err) {
        if(err) {
            return console.log(err);
        }
    });
});