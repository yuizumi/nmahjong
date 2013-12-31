// html
(function () {
    var html = {};
    this.html = html;

    _.templateSettings.escape = /{{([^{][\s\S]*?)}}/g;

    var get_ = _.memoize(function (name) {
        return _.template($('#tmpl_' + name).text().trim());
    });

    html.render = function (name, data) {
        return (get_(name))({$_: data, $r: html.render}).trim();
    };
})();

// manip
(function () {
    var manip = {};
    this.manip = manip;

    var TILE_IDS = (function () {
        var tiles = [
            'T1', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'T8', 'T9',
            'S1', 'S2', 'S3', 'S4', 'S5', 'S6', 'S7', 'S8', 'S9',
            'W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'W8', 'W9',
            'FE', 'FS', 'FW', 'FN', 'JP', 'JF', 'JC',
        ];
        return _.object(
            _.map(tiles, function (tile, index) { return [tile, index]; }));
    })();

    manip.hand = function (tiles) {
        return _.sortBy(tiles, function (tile) {
            if (!tile) return -1;
            return (nmj.has(tile, ~nmj.RED)) ? 99 : TILE_IDS[tile.BaseTile];
        });
    };

    manip.meld = function (meld, playerId) {
        var tiles = meld.AnnotatedTiles;
        if (meld.Type == 'ConcealedKong') {
            var sorted = _.sortBy(
                tiles, function (tile) { return nmj.has(tile, nmj.RED); });
            return [null, sorted[2], sorted[3], null];
        } else {
            var upright = _.reject(
                tiles, function (tile) { return nmj.has(tile, ~nmj.RED); });
            var rotated = _.select(
                tiles, function (tile) { return nmj.has(tile, ~nmj.RED); });
            var split = (playerId - meld.Feeder.Id + 3) % 4;
            if (split == 2) split = upright.length;
            return [].concat(
                upright.slice(0, split), rotated, upright.slice(split));
        }
    };

    manip.discards = function (tiles, withClaimed) {
        var tweaked = [];
        var riichi = 0;
        _.each(tiles, function (tile) {
            if (nmj.has(tile, nmj.RIICHI)) {
                riichi = nmj.RIICHI;
            }
            if (nmj.has(tile, nmj.CLAIMED) && !withClaimed) {
                return;  // continue
            }
            tweaked.push({
                BaseTile: tile.BaseTile, Annotations: tile.Annotations | riichi});
            riichi = 0;
        });
        return [tweaked.slice(0, 6), tweaked.slice(6, 12), tweaked.slice(12)];
    };
})();

// nmj
(function () {
    var nmj;
    this.nmj = nmj = {
        RED: 1, DRAWN: 2, CLAIMED: 4, EXTENDING: 8, RIICHI: 16,
    };

    nmj.has = function (tile, annots) {
        return tile.Annotations & annots;
    };

    nmj.wind = function (enumIndex) {
        return ['East', 'South', 'West', 'North'][enumIndex];
    }
})();

// ui
(function () {
    var ui = {};
    this.ui = ui;

    var records_, viewpoint_ = 0;

    ui.load = function (file) {
        var reader = new FileReader();
        reader.onload = function () {
            parse_(reader.result);
        };
        reader.onerror = function () {
            error_('Could not open the specified file.');
        };
        reader.readAsText(file, 'UTF-8');
    };

    ui.step = function (delta) {
        setIndex_(getIndex_() + delta);
    };

    ui.jump = function (select) {
        setIndex_($(select).val());
    };

    ui.rotate = function () {
        viewpoint_ = (viewpoint_ + 1) % 4;
        ui.redraw();
    };

    ui.redraw = function () {
        $('#game-table').html(
            html.render('GameTable', records_[getIndex_()].state));
    };

    ui.viewpoint = function () {
        return viewpoint_;
    }

    ui.enabled = function (name) {
        return $('#' + name).prop('checked');
    }

    function error_(message) {
        $('#message').html(html.render('Error', message));
    }

    function getIndex_() {
        return parseInt($('#index').val());
    }

    function setIndex_(index) {
        index = Math.max(0, Math.min(index, records_.length - 1));
        $('#index').val(index);
        ui.redraw();
        $('#message').html(html.render('Event', records_[index]));
        var jumpIndex = _.sortedIndex(_.pluck($('#jump option'), 'value'), index + 1) - 1;
        $('#jump').prop('selectedIndex', jumpIndex);
    }

    function parse_(content) {
        var records, jump;
        try {
            records = JSON.parse(content);
            jump = buildJump_(records);
        } catch (e) {
            error_('Failed to open the file: ' + e.message);
            return;
        }

        $('#jump').html(jump);
        records_ = records;
        setIndex_(0);
        $('#controls').show();
    }

    function buildJump_(records) {
        var hands = [];
        var value = 0;
        _.each(records, function (record, index) {
            if (record.event == 'HandStarting') {
                value = index;
            }
            if (record.event == 'HandStarted') {
                hands.push({value: value, state: record.state});
            }
        });
        return html.render('Jump', hands);
    }
})();
