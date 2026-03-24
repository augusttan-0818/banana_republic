
/**
    Generates an agenda package for each row selected, then returns a URL to the generated object.

    @param selectedRDNumbers - Array containing IDs for each Ref. Doc to be added to an agenda package
    @returns A string url to the resulting blob HTML
*/
export function generateRDAgendaPackage(selectedRDNumbers: string[]): string {
    console.log(`GENERATE WORKFLOW STARTED! :>`);
    console.log(selectedRDNumbers);

    // Once database schema is solidified, remove capitalized words and replace formatting with real values
    // TODO: paramaterize the list of updates.
    const DATE_CREATED = 'DATE CREATED YYYY-MM-DD';
    const DATE_LAST_MODIFIED = 'DATE LAST MODIFIED YYYY-MM-DD HH:MM:SS';
    const DATE_GENERATED = 'DATE GENERATED YYYY-MM-DDTHH:MM:SS';
    const UPDATE_NUMBER = 'UPDATE NUMBER ###';
    const UPDATED_BY = 'UPDATED BY name@name.ca';
    // const ISSUING_AGENCY = 'ISSUING AGENCY SHORT NAME name'
    const ISSUING_AGENCY_FULL_NAME = 'ISSUING AGENCY LONG NAME name';
    const RD_UPDATE_STATUS = 'RD UPDATE STATUS status';
    const RD_PUBLICATION_DATE = 'RD PUBLICATION DATE YYYY-MM-DD';
    const REFERENCED_IN = 'REFERENCED IN []';
    const STANDING_COMMITTEES = 'STANDING COMMITTEES []';
    const PROCESS_ENTRIES = Array(Math.floor(Math.random()*10)).fill(`YYYY-MM-DD &mdash; STATUS`);

    const doc = `
        <!DOCTYPE html>
        <html>
            <head>
                <meta charset="utf-8" />
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
                <meta name="dc.creator" content="Codes Canada" />
                <meta name="dc.publisher" content="Government of Canada National Research Council Canada" />
                <meta name="dcterms.language" title="ISO639-2" content="eng" />
                <meta name="dc.date.created" content="${DATE_CREATED}" />
                <meta name="dc.date.modified" content="${DATE_LAST_MODIFIED}" />
                <meta name="date" content="${DATE_GENERATED}" /> <!-- for Prince -->
                <meta name="generator" content="CCC Teamsite CMS + PrinceXML"/>
                <style type="text/css">
                    @page {
                        size: Letter;
                        margin-left: 1.5cm;
                        margin-top: 1.80cm;
                        margin-right: 1.5cm;
                        margin-bottom: 2.54cm;
                        @top-left {
                            font-family: "Arial";
                            font-size: 10pt;
                            content: "Canadian Board for Harmonized Construction Codes"
                        }
                        @top-right {
                            font-family: "Arial";
                            font-size: 10pt;
                            content: "Update ${UPDATE_NUMBER}"
                        }
                        @bottom-left {
                            font-family: "Arial";
                            font-size: 10pt;
                            content: "Last modified: ${DATE_LAST_MODIFIED}";
                        }
                        @bottom-right {
                            font-family: "Arial";
                            font-size: 10pt;
                            content: "Page " counter(page) "/" counter(pages);
                            white-space: pre-line;
                        }
                    }
                    body {
                        margin: 0;
                        padding: 0;
                        font-family: "Arial";
                        font-size: 10pt;
                        text-align: justify;
                    }
                    h1 {
                        font-size: 13pt;
                        margin-bottom: 5px;
                    }
                    h2 {
                        font-size: 12pt;
                        margin-bottom: 0px;
                        border-top: 1px solid #000;
                        border-bottom: 1px solid #000;
                        padding-top: 5px;
                        padding-bottom: 5px;
                        background-color: inherit;
                    }
                    h3 {
                        font-size: 10pt;
                        margin-bottom: 0px;
                    }
                    table {
                        border: none;
                    }
                    th, td {
                        text-align: left;
                        vertical-align: top;
                        font-weight: normal;
                    }
                    del {
                        font-weight: bold;
                        color: red;
                        text-decoration: line-through;
                    }
                    ins {
                        font-weight: bold;
                        color: green;
                    }
                    p {
                        margin-top: 5px;
                    }
                    .FLITE-Tracking .ice-ins:not(.flite-container-only) {
                        background-color: rgb(160, 247, 165) !important;
                    }
                    .FLITE-Tracking .ice-del:not(.flite-container-only) {
                        background-color: rgb(255, 221, 221) !important;
                    }				
                </style>
                <title>Update ${UPDATE_NUMBER}</title>
            </head>
            <body class="FLITE-Tracking">
                <h1>Update to ANSI Z21.13-2014/CSA 4.9-2014 &mdash; Gas-Fired Low Pressure Steam and Hot Water Boilers</h1>
                <table>
                    <tr>
                        <th>Update No.:</th>
                        <td>${UPDATE_NUMBER}</td>
                    </tr>
                    <tr>
                        <th>Date of Update:</th>
                        <td>${DATE_CREATED}</td>
                    </tr>
                    <tr>
                        <th>Updated By:</th>
                        <td>${UPDATED_BY}</td>
                    </tr>
                    <tr>
                        <th>Issuing Agency:</th>
                        <td>${ISSUING_AGENCY_FULL_NAME}</td>
                    </tr>
                    <tr>
                        <th>Status:</th>
                        <td>${RD_UPDATE_STATUS}</td>
                    </tr>
                    <tr>
                        <th>Publication Date:</th>
                        <td>${RD_PUBLICATION_DATE}</td>
                    </tr>
                    <tr>
                        <th>Referenced in:</th>
                        <td>${REFERENCED_IN}</td>
                    </tr>
                    <tr>
                        <th>Standing Committee(s):</th>
                        <td>${STANDING_COMMITTEES}</td>
                    </tr>
                </table>

                <h2>Process</h2>
                    ${PROCESS_ENTRIES.map(value => `<h3>${value}</h3>`).join('\n')}

                <h2>Update ${UPDATE_NUMBER}</h2>

                    <h3>Updated Document Number As Submitted By Issuing Agency</h3>
                    <p>ANSI Z21.13-<del>2014</del><ins>2017</ins>/CSA 4.9-<del>2014</del><ins>2017</ins></p>   

                    <h3>Updated Document Number (Current Version)</h3>
                    <p>ANSI Z21.13-<del>2014</del><ins>2017</ins>/CSA 4.9-<del>2014</del><ins>2017</ins></p>

                    <h3>Updated Document Title As Submitted By Issuing Agency</h3>
                    <p>Gas-Fired Low Pressure Steam and Hot Water Boilers</p>      

                    <h3>Updated Document Title (Current Version)</h3>
                    <p>Gas-Fired Low Pressure Steam and Hot Water Boilers</p>
            </body>
        </html>`

    const blob = new Blob([doc], { type: 'text/html;charset=utf8'});
    const url = URL.createObjectURL(blob)

    return url;
}
